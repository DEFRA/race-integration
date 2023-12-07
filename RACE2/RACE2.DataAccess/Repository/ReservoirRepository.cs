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
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
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
        public async Task<Reservoir> UpdateReservoir(ReservoirUpdateDetailsDTO updatedReservoir)
        {
            try
            {
                using (var conn = Connection)
                {
                    int id = updatedReservoir.Id;
                    string updateQuery = @"UPDATE Reservoirs SET PublicName = @PublicName,GridReference=@GridReference,NearestTown=@NearestTown WHERE Id = @Id";
                    var parameters = new DynamicParameters();
                    parameters.Add("Id", id, DbType.Int32);
                    parameters.Add("PublicName", updatedReservoir.PublicName, DbType.String);
                    parameters.Add("GridReference", updatedReservoir.GridReference, DbType.String);
                    parameters.Add("NearestTown", updatedReservoir.NearestTown, DbType.String);
                    var reservoirUpdatedCount = await conn.ExecuteAsync(updateQuery, parameters);
                    if (reservoirUpdatedCount == 1)
                    {
                        var reservoirUpdated = await GetReservoirById(id);
                        return reservoirUpdated;
                    }
                    else
                    {
                        return null;
                    }                    
                }
            }
            catch (Exception ex) 
            {
                string err = ex.Message;
                return null;
            };
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
                        var reservoirs = await conn.QueryAsync<ReservoirDetailsDTO,UserDetail, ReservoirDetailsDTO>("sp_GetReservoirsbyUserId", (reservoir,userdetail) =>
                        {
                           // reservoir.Address = address;
                            reservoir.UserDetail = userdetail;

                            return reservoir;
                        }, parameters, null, true, splitOn: "ReservoirId", commandType: CommandType.StoredProcedure);
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

        public async Task<List<SubmissionStatusDTO>> GetReservoirStatusByUserId(int id)
        {
            _logger.LogInformation("Getting Reservoir status for the user {id}", id);
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("id", id, DbType.String);
                    if (id != 0)
                    {

                        var operatorlist = await conn.QueryAsync<SubmissionStatusDTO>("sp_GetReservoirStatusByUserId", parameters, commandType: CommandType.StoredProcedure);

                        return operatorlist.ToList();
                    }

                    else
                    {
                        _logger.LogInformation($"The input is not valid or null {id}");
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

        public async Task<List<UndertakerDTO>> GetUndertakerforReservoir(int id)
        {
            _logger.LogInformation("Getting Undertaker details of the reservoirs for the assigned engineer {id} ", id);
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("id", id, DbType.Int64);
                    if (id != 0)
                    {

                        var operatorlist = await conn.QueryAsync<UndertakerDTO>("sp_GetUndertakerforReservoir", parameters, commandType: CommandType.StoredProcedure);

                        return operatorlist.ToList();
                    }
                   else
                    {
                        _logger.LogInformation($"The input is not valid {id}",id);
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


        public async Task<SubmissionStatus> UpdateReservoirStatus(int reservoirid, int userid, string reportStatus)
        {
            _logger.LogInformation("Updating reservoir status for the reservoir  {reservoirid} by the {userid}  ", reservoirid, userid);
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("reservoirid", reservoirid, DbType.Int64);
                    parameters.Add("userid", userid, DbType.Int64);
                    parameters.Add("reportStatus", reportStatus, DbType.String);
                    if (reservoirid != 0)
                    {

                        var operatorlist = await conn.QueryAsync<SubmissionStatus>("sp_UpdateReservoirStatus", parameters, commandType: CommandType.StoredProcedure);

                        return operatorlist.FirstOrDefault();
                    }
                    else
                    {
                        _logger.LogInformation("The input is not valid {reservoirid} by the {userid}  ", reservoirid, userid);
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


        public async Task<int> InsertUploadDocumentDetails(DocumentDTO document)
        {
            int result = 0;
            _logger.LogInformation("Inserting uploaded document details for the reservoir" );
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("filename", document.FileName, DbType.String);
                    parameters.Add("filelocation", document.FileLocation, DbType.String);
                    parameters.Add("filetype", document.FileType, DbType.String);
                    parameters.Add("documenttype", document.DocumentType, DbType.String);
                    parameters.Add("suppliedby", document.SuppliedBy, DbType.Int64);
                    parameters.Add("suppliedviaservice", document.SuppliedViaService, DbType.Int64);
                    parameters.Add("datesent", document.DateSent, DbType.DateTime);
                    parameters.Add("reservoirid", document.ReservoirId, DbType.Int64);
                    //parameters.Add("submissionid", document.SubmissionId, DbType.Int64);
                    parameters.Add("documentName",document.DocumentName,DbType.String);
                    parameters.Add("blobStorageFileName",document.BlobStorageFileName, DbType.String);
                    if (document != null)
                    {

                        result  = await conn.ExecuteAsync("sp_InsertDocumentUpload", parameters, commandType: CommandType.StoredProcedure);

                        return result;
                    }
                    else
                    {
                        _logger.LogInformation("The input is not valid ");
                        return result;
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 1;
            }
        }


        public async Task<int> UpdateScannedDocumentResult(DateTime scanneddatetime, bool isClean, string uploadblobpath , string blobStorageFileName)
        {
            _logger.LogInformation("Updating scan result for the reservoir  {documentName}  ", blobStorageFileName);
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("scannedtime", scanneddatetime, DbType.DateTime);
                    parameters.Add("isClean", isClean, DbType.Boolean);
                    parameters.Add("uploadBlobpath", uploadblobpath, DbType.String);
                    parameters.Add("documentName", blobStorageFileName, DbType.String);
                    if (blobStorageFileName != null)
                    {

                         await conn.ExecuteAsync("sp_UpdateScannedDocumentResult", parameters, commandType: CommandType.StoredProcedure);

                        return 1;
                    }
                    else
                    {
                        _logger.LogInformation("The input is not valid {documentName}", blobStorageFileName);
                        return 0;
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 0;
            }
        }


        public async Task<DocumentDTO> GetScannedResultbyDocId(int id)
        {
            _logger.LogInformation("Get scan result for the document  {id}  ", id);
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("docid",id, DbType.Int64);
                    
                    if (id != 0)
                    {

                        var result = await conn.QueryAsync<DocumentDTO>("sp_GetScannedResultByDocId", parameters, commandType: CommandType.StoredProcedure);

                        return result.FirstOrDefault();
                    }
                    else
                    {
                        _logger.LogInformation("The input is not valid {id}", id);
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

        public async Task<int> InsertDocumentRelatedTable(int reservoirid, int submissionid, int documentid)
        {
            _logger.LogInformation("Insert document related table for the document  {id}  ", documentid);
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("documentid", documentid, DbType.Int64);
                    parameters.Add("submissionid", submissionid, DbType.Int64);
                    parameters.Add("reservoirid", documentid, DbType.Int64);

                    if (documentid != 0)
                    {

                        var result = await conn.ExecuteAsync("sp_InsertDocumentRelatedTable", parameters, commandType: CommandType.StoredProcedure);

                        return 1;
                    }
                    else
                    {
                        _logger.LogInformation("The input is not valid {id}", documentid);
                        return 0;
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 0;
            }
        }

    }
}
