using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RACE2.Common;
using RACE2.DataModel;
using RACE2.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RACE2.DataAccess.Repository
{
    public class ReservoirRepository: IReservoirRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly IConfiguration _configuration;
        public ReservoirRepository(IConfiguration configuration, ILogger<UserRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_configuration["SqlConnectionString"]);
            }
        }

        public async Task<Reservoir> GetReservoirById(int id)
        {
            using (var conn = Connection)
            {
                var query = @"Select *
                               from Reservoirs A                                   
                               Where A.Id=@Id";
                var reservoir = await conn.QuerySingleAsync<Reservoir>(query, new { Id = id });
                return reservoir;
            }
        }
        public async Task<Reservoir> UpdateReservoir(int id, Reservoir reservoir)
        {
            using (var conn = Connection)
            {
                string updateQuery = @"UPDATE [dbo].Reservoir SET PublicName = @PublicName WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("Id", reservoir.Id, DbType.Int32);
                parameters.Add("PublicName", reservoir.PublicName, DbType.String);
                parameters.Add("GridReference", reservoir.GridReference, DbType.String);
                parameters.Add("NearestTown", reservoir.NearestTown, DbType.String);
                var reservirUpdated = await conn.QuerySingleAsync<Reservoir>(updateQuery, parameters);
                return reservirUpdated;
            }
        }

        ////public async Task<UserDetail> GetReservoirsByUserId(int id)
        //{
        //    using (var conn = Connection)
        //    {
        //        var query = @"Select A.Id, A.*,B.UserDetailId,B.ReservoirId,c.Id, c.*,d.id,d.AddressLine1,d.AddressLine2,d.town,d.postcode,d.county
        //                      from AspNetUsers A 
        //                      inner join UserReservoirs B ON  A.Id =b.UserDetailId 
        //                      inner join Reservoirs c On c.Id = b.ReservoirId 
        //                      inner join Addresses d On d.id = c.addressid
        //                      Where A.Id=@Id";

        //        var parameters = new DynamicParameters();
        //        parameters.Add("Id", id, DbType.Int32);
        //        var users = await conn.QueryAsync<UserDetail, UserReservoir, Address, UserDetail>(query, (user, reservoir, address) =>
        //        {
        //            reservoir.Reservoir.address = address;
        //            user.Reservoirs.Add(reservoir);

        //            return user;
        //        }, parameters, splitOn: "ReservoirId,id");
        //        var result = users.GroupBy(u => u.Id).Select(g =>
        //        {
        //            var groupedUser = g.First();
        //            groupedUser.Reservoirs = g.Select(u => u.Reservoirs.FirstOrDefault()).ToList();
        //            return groupedUser;
        //        });
        //        return result.FirstOrDefault();
        //    }
        //}

        public async Task<List<ReservoirDetailsDTO>> GetReservoirsByUserId(int id)
        {
            _logger.LogInformation("Getting Reservoir details for the user {id} ", id);

            try
            {
                if (id != 0)
                {
                    using (var conn = Connection)
                    {

                        var parameters = new DynamicParameters();
                        parameters.Add("id", id, DbType.String);
                        var reservoirs = await conn.QueryAsync<ReservoirDetailsDTO, Address, ReservoirDetailsDTO>("sp_GetReservoirsbyUserId", (reservoir, address) =>
                        {
                            reservoir.Address = address;

                            return reservoir;
                        }, parameters, null, true, splitOn: "ReservoirId,id", commandType: CommandType.StoredProcedure);
                        return reservoirs.ToList();
                    }
                }
                else
                {
                    _logger.LogInformation("THe input is not valid");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<List<DataModel.Action>> GetActionsListByReservoirIdAndCategory(int reservoirid, int category)
        {
            var strCategory = (Category)category;
            using (var conn = Connection)
            {

                var parameters = new DynamicParameters();
                parameters.Add("reservoirid", reservoirid, DbType.Int64);
                parameters.Add("category", strCategory.ToString(), DbType.String);
                var actionlist = await conn.QueryAsync<DataModel.Action>("sp_GetActionsListByReservoirId", parameters, commandType: CommandType.StoredProcedure);
                return actionlist.ToList();
            }
        }

        public async Task<List<SafetyMeasure>> GetSafetyMeasuresListByReservoirId(int reservoirid)
        {
            using (var conn = Connection)
            {

                var parameters = new DynamicParameters();
                parameters.Add("reservoirid", reservoirid, DbType.Int64);
                var actionlist = await conn.QueryAsync<SafetyMeasure>("sp_GetSafetyMeasuresListByReservoirId", parameters, commandType: CommandType.StoredProcedure);
                return actionlist.ToList();
            }
        }

        public async Task<Address> GetAddressByReservoirId(int reservoirid, string operatortype)
        {
            _logger.LogInformation("Getting Operator details of the reservoir {reservoirid} with operatorType as {operatortype}", reservoirid, operatortype);
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("reservoirid", reservoirid, DbType.Int64);
                    if (operatortype != null && operatortype == "Organisation")
                    {

                        var OrgAddress = await conn.QueryAsync<Address>("sp_GetAddressForReservoir", parameters, commandType: CommandType.StoredProcedure);

                        return OrgAddress.FirstOrDefault();
                    }
                    else if (operatortype != null && operatortype == "Individual")
                    {

                        var OrgAddress = await conn.QueryAsync<Address>("sp_GetAddressForReservoir", parameters, commandType: CommandType.StoredProcedure);

                        return OrgAddress.FirstOrDefault();
                    }
                    else
                    {
                        _logger.LogInformation($"The input is not valid {operatortype}");
                        return null;
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }



        }

        public async Task<List<OperatorDTO>> GetOperatorsforReservoir(int reservoirid, string operatortype)
        {
            _logger.LogInformation("Getting Operator details of the reservoir {reservoirid} with operatorType as {operatortype}", reservoirid, operatortype);
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("reservoirid", reservoirid, DbType.Int64);
                    if (operatortype != null && operatortype == "Organisation")
                    {

                        var operatorlist = await conn.QueryAsync<OperatorDTO>("sp_GetOperatorListAsOrgForReservoir", parameters, commandType: CommandType.StoredProcedure);

                        return operatorlist.ToList();
                    }
                    else if (operatortype != null && operatortype == "Individual")
                    {

                        var operatorlist = await conn.QueryAsync<OperatorDTO>("sp_GetOperatorListAsIndiviForReservoir", parameters, commandType: CommandType.StoredProcedure);

                        return operatorlist.ToList();
                    }
                    else
                    {
                        _logger.LogInformation($"The input is not valid {operatortype}");
                        return null;
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }


        }

        public async Task<List<SubmissionStatusDTO>> GetReservoirStatusByEmail(string email)
        {
            _logger.LogInformation("Getting Reservoir status for the user {email}", email);
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("email", email, DbType.String);
                    if (email != null)
                    {

                        var operatorlist = await conn.QueryAsync<SubmissionStatusDTO>("sp_GetReservoirStatusByEmail", parameters, commandType: CommandType.StoredProcedure);

                        return operatorlist.ToList();
                    }

                    else
                    {
                        _logger.LogInformation($"The input is not valid or null {email}");
                        return null;
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }

        }
    }
}
