
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RACE2.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
//using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Identity;
//using RACE2.Logging.Service;
using RACE2.Dto;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using RACE2.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
//using RACE2.Logging;

namespace RACE2.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration, ILogger<UserRepository> logger)
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

        public async Task<IEnumerable<UserDetail>> GetUserDetails()
        {
            var query = "SELECT * FROM AspNetUsers";
            using (var conn = Connection)
            {
                var companies = await conn.QueryAsync<UserDetail>(query);
                return companies.ToList();
            }
        }

        public async Task<UserDetail> GetUserById(int id)
        {
            using (var conn = Connection)
            {
                var query = "Select * from AspNetUsers where Id=@id";
                var user = await conn.QuerySingleAsync<UserDetail>(query, new { Id = id });
                return user;
            }
        }

        public async Task<UserSpecificDto> GetUserByEmailID(string email)
        {

            // _logService.Write("Repository");
            try
            {
                using (var conn = Connection)
                {

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("Email", email, DbType.String);

                    var user = await conn.QueryAsync<UserSpecificDto, Address, Role, UserSpecificDto>("sp_GetUserByEmailID", (user, address, role) =>
                    {
                        user.addresses.Add(address);
                        user.roles.Add(role);

                        return user;

                    }, parameters, null, true, splitOn: "Id,Addressid,Id", commandType: CommandType.StoredProcedure);
                    var result = user.GroupBy(u => u.Id).Select(g =>
                    {
                        var groupedUser = g.First();
                        groupedUser.addresses = g.Select(u => u.addresses.Single()).ToList();
                        return groupedUser;
                    });
                    return result.FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UserSpecificDto> GetUserWithRoles(string email)
        {
            try
            {
                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();

                    parameters.Add("Email", email, DbType.String);

                    var users = await conn.QueryAsync<UserSpecificDto, Role, UserSpecificDto>("sp_GetUserWithRoles", (user, role) =>
                    {
                        user.roles.Add(role);
                        //user.
                        return user;
                    }, parameters, null, true, splitOn: "RoleId", null, CommandType.StoredProcedure);

                    var result = users.GroupBy(u => u.Id).Select(g =>
                    {
                        var groupedUser = g.First();
                       groupedUser.roles = g.Select(u => u.roles.Single()).ToList();
                        return groupedUser;
                    });
                    return result.FirstOrDefault();

                   
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        //public async Task<UserDetail> CreateUser(UserDetail newuser)
        //{
        //    var query = "INSERT INTO AspNetUsers (c_defra_id,c_type,c_display_name,c_first_name,c_last_name,c_status,c_created_on_date,c_last_access_date,c_password_retry_count,EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount) VALUES (@c_defra_id,@c_type,@c_display_name,@c_first_name,@c_last_name,@c_status,@c_created_on_date,@c_last_access_date,@c_password_retry_count,@EmailConfirmed,@PhoneNumberConfirmed,@TwoFactorEnabled,@LockoutEnabled,@AccessFailedCount)"
        //        + "SELECT CAST(SCOPE_IDENTITY() as int)";
        //    var parameters = new DynamicParameters();
        //    //parameters.Add("Id", newuser.Id, DbType.String);
        //    parameters.Add("c_defra_id", newuser.c_defra_id, DbType.String);
        //    parameters.Add("c_type", newuser.c_type, DbType.String);
        //    //  parameters.Add("c_display_name", newuser.c_display_name, DbType.String);
        //    parameters.Add("c_first_name", newuser.c_first_name, DbType.String);
        //    parameters.Add("c_last_name", newuser.c_last_name, DbType.String);
        //    parameters.Add("c_status", newuser.c_status, DbType.String);
        //    parameters.Add("c_created_on_date", newuser.c_created_on_date, DbType.DateTime);
        //    parameters.Add("c_last_access_date", newuser.c_last_access_date, DbType.DateTime);
        //    // parameters.Add("c_password_retry_count", newuser.c_password_retry_count, DbType.Int32);
        //    parameters.Add("EmailConfirmed", newuser.EmailConfirmed, DbType.Byte);
        //    parameters.Add("PhoneNumberConfirmed", newuser.PhoneNumberConfirmed, DbType.Byte);
        //    parameters.Add("TwoFactorEnabled", newuser.TwoFactorEnabled, DbType.Byte);
        //    parameters.Add("LockoutEnabled", newuser.LockoutEnabled, DbType.Byte);
        //    parameters.Add("AccessFailedCount", newuser.AccessFailedCount, DbType.Int32);

        //    using (var conn = Connection)
        //    {
        //        var id = await conn.QuerySingleAsync<int>(query, parameters);
        //        var createdCompany = new UserDetail
        //        {
        //            Id = newuser.Id,
        //            c_defra_id = newuser.c_defra_id,
        //            c_type = newuser.c_type,
        //            //  c_display_name = newuser.c_display_name,
        //            c_first_name = newuser.c_first_name,
        //            c_last_name = newuser.c_last_name,
        //            c_status = newuser.c_status,
        //            c_created_on_date = newuser.c_created_on_date,
        //            c_last_access_date = newuser.c_last_access_date,
        //            //  c_password_retry_count = newuser.c_password_retry_count,
        //            EmailConfirmed = newuser.EmailConfirmed,
        //            PhoneNumberConfirmed = newuser.PhoneNumberConfirmed,
        //            TwoFactorEnabled = newuser.TwoFactorEnabled,
        //            LockoutEnabled = newuser.LockoutEnabled,
        //            AccessFailedCount = newuser.AccessFailedCount

        //        };
        //        return createdCompany;
        //    }
        //}

        //public async Task<UserDetail> ValidateUser(UserDetail loginuser)
        //{
        //    using (var conn = Connection)
        //    {
        //        var res = await GetUserByEmailID(loginuser.Email);
        //        if (res == null)
        //        {
        //            var query = "INSERT INTO AspNetUsers (c_defra_id,c_type,c_display_name,c_first_name,c_last_name,c_mobile,c_emergency_phone,c_organisation_id,c_organisation_name,c_job_title,c_current_panel,c_paon,c_saon,c_status,c_created_on_date,c_last_access_date,c_password_retry_count,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount) VALUES (@c_defra_id,@c_type,@c_display_name,@c_first_name,@c_last_name,@c_mobile,@c_emergency_phone,@c_organisation_id,@c_organisation_name,@c_job_title,@c_current_panel,@c_paon,@c_saon,@c_status,@c_created_on_date,@c_last_access_date,@c_password_retry_count,@UserName,@NormalizedUserName,@Email,@NormalizedEmail,@EmailConfirmed,@PhoneNumber,@PhoneNumberConfirmed,@TwoFactorEnabled,@LockoutEnabled,@AccessFailedCount)"
        //        + "SELECT CAST(SCOPE_IDENTITY() as int)";
        //            var parameters = new DynamicParameters();
        //            //parameters.Add("Id", newuser.Id, DbType.String);
        //            parameters.Add("c_defra_id", loginuser.c_defra_id, DbType.String);
        //            parameters.Add("c_type", loginuser.c_type, DbType.String);
        //            //  parameters.Add("c_display_name", loginuser.c_display_name, DbType.String);
        //            parameters.Add("c_first_name", loginuser.c_first_name, DbType.String);
        //            parameters.Add("c_last_name", loginuser.c_last_name, DbType.String);
        //            parameters.Add("c_mobile", loginuser.c_mobile, DbType.String);
        //            parameters.Add("c_emergency_phone", loginuser.c_emergency_phone, DbType.String);
        //            // parameters.Add("c_organisation_id", loginuser.c_organisation_id, DbType.String);
        //            // parameters.Add("c_organisation_name", loginuser.c_organisation_name, DbType.String);
        //            parameters.Add("c_job_title", loginuser.c_job_title, DbType.String);
        //            parameters.Add("c_current_panel", loginuser.c_current_panel, DbType.String);
        //            parameters.Add("c_paon", loginuser.c_paon, DbType.String);
        //            parameters.Add("c_saon", loginuser.c_saon, DbType.String);
        //            parameters.Add("c_status", loginuser.c_status, DbType.String);
        //            parameters.Add("c_created_on_date", loginuser.c_created_on_date, DbType.DateTime);
        //            parameters.Add("c_last_access_date", loginuser.c_last_access_date, DbType.DateTime);
        //            // parameters.Add("c_password_retry_count", loginuser.c_password_retry_count, DbType.Int32);
        //            parameters.Add("UserName", loginuser.UserName, DbType.String);
        //            parameters.Add("NormalizedUserName", loginuser.NormalizedUserName, DbType.String);
        //            parameters.Add("Email", loginuser.Email, DbType.String);
        //            parameters.Add("NormalizedEmail", loginuser.NormalizedEmail, DbType.String);
        //            parameters.Add("EmailConfirmed", loginuser.EmailConfirmed, DbType.Byte);
        //            parameters.Add("PhoneNumber", loginuser.PhoneNumber, DbType.String);
        //            parameters.Add("PhoneNumberConfirmed", loginuser.PhoneNumberConfirmed, DbType.Byte);
        //            parameters.Add("TwoFactorEnabled", loginuser.TwoFactorEnabled, DbType.Byte);
        //            parameters.Add("LockoutEnabled", loginuser.LockoutEnabled, DbType.Byte);
        //            parameters.Add("AccessFailedCount", loginuser.AccessFailedCount, DbType.Int32);

        //            int id = await conn.ExecuteScalarAsync<int>(query, loginuser);
        //            return await GetUserById(id);

        //        }
        //        else
        //        {
        //            var query = @"UPDATE AspNetUsers
        //                        SET c_defra_id=@c_defra_id,c_type=@c_type,c_display_name=@c_display_name,
        //                            c_first_name=@c_first_name,c_last_name=@c_last_name,c_mobile=@c_mobile,c_emergency_phone=@c_emergency_phone,
        //                            c_organisation_id=@c_organisation_id,c_organisation_name=@c_organisation_name,c_job_title=@c_job_title,
        //                            c_current_panel=@c_current_panel,c_paon=@c_paon,c_saon=@c_saon,c_status=@c_status,
        //                            c_created_on_date=@c_created_on_date,c_last_access_date=@c_last_access_date,
        //                            c_password_retry_count=@c_password_retry_count,UserName=@UserName,NormalizedUserName=@NormalizedUserName,
        //                            Email=@Email,NormalizedEmail=@NormalizedEmail,EmailConfirmed=@EmailConfirmed,PhoneNumber=@PhoneNumber,PhoneNumberConfirmed=@PhoneNumberConfirmed,TwoFactorEnabled=@TwoFactorEnabled,LockoutEnabled=@LockoutEnabled,AccessFailedCount=@AccessFailedCount
        //                        WHERE Id=@Id";


        //            var parameters = new DynamicParameters();
        //            parameters.Add("Id", res.Id, DbType.String);
        //            parameters.Add("c_defra_id", loginuser.c_defra_id, DbType.String);
        //            parameters.Add("c_type", loginuser.c_type, DbType.String);
        //            // parameters.Add("c_display_name", loginuser.c_display_name, DbType.String);
        //            parameters.Add("c_first_name", loginuser.c_first_name, DbType.String);
        //            parameters.Add("c_last_name", loginuser.c_last_name, DbType.String);
        //            parameters.Add("c_mobile", loginuser.c_mobile, DbType.String);
        //            parameters.Add("c_emergency_phone", loginuser.c_emergency_phone, DbType.String);
        //            // parameters.Add("c_organisation_id", loginuser.c_organisation_id, DbType.String);
        //            // parameters.Add("c_organisation_name", loginuser.c_organisation_name, DbType.String);
        //            parameters.Add("c_job_title", loginuser.c_job_title, DbType.String);
        //            parameters.Add("c_current_panel", loginuser.c_current_panel, DbType.String);
        //            parameters.Add("c_paon", loginuser.c_paon, DbType.String);
        //            parameters.Add("c_saon", loginuser.c_saon, DbType.String);
        //            parameters.Add("c_status", loginuser.c_status, DbType.String);
        //            parameters.Add("c_created_on_date", loginuser.c_created_on_date, DbType.DateTime);
        //            parameters.Add("c_last_access_date", loginuser.c_last_access_date, DbType.DateTime);
        //            // parameters.Add("c_password_retry_count", loginuser.c_password_retry_count, DbType.Int32);
        //            parameters.Add("UserName", loginuser.UserName, DbType.String);
        //            parameters.Add("NormalizedUserName", loginuser.NormalizedUserName, DbType.String);
        //            parameters.Add("Email", loginuser.Email, DbType.String);
        //            parameters.Add("NormalizedEmail", loginuser.NormalizedEmail, DbType.String);
        //            parameters.Add("EmailConfirmed", loginuser.EmailConfirmed, DbType.Byte);
        //            parameters.Add("PhoneNumber", loginuser.PhoneNumber, DbType.String);
        //            parameters.Add("PhoneNumberConfirmed", loginuser.PhoneNumberConfirmed, DbType.Byte);
        //            parameters.Add("TwoFactorEnabled", loginuser.TwoFactorEnabled, DbType.Byte);
        //            parameters.Add("LockoutEnabled", loginuser.LockoutEnabled, DbType.Byte);
        //            parameters.Add("AccessFailedCount", loginuser.AccessFailedCount, DbType.Int32);

        //            await conn.ExecuteAsync(query, parameters);
        //            return await GetUserById(loginuser.Id);
        //        }
        //    }
        //}

        public async Task<UserDetail> MatchUserWithEmailAndPasswordHash(string email, string passwordhash)
        {
            var query = @"SELECT * from AspNetUsers                                
                            WHERE Email=@Email and PasswordHash=@PasswordHash";
            using (var conn = Connection)
            {
                var parameters = new DynamicParameters();
                parameters.Add("Email", email, DbType.String);
                parameters.Add("PasswordHash", passwordhash, DbType.String);
                var result = await conn.ExecuteAsync(query, parameters);
                if (result == 0)
                {
                    return null;
                }
                else
                {
                    return await GetUserByEmailID(email);
                }
            }
        }

        public async Task<UserDetail> UpdatePasswordHashForUser(int id, string passwordhash)
        {
            var user = await GetUserById(id);
            if (user == null)
            {
                return null;
            }
            else
            {
                var query = @"UPDATE AspNetUsers
                                SET PasswordHash=@PasswordHash
                                WHERE Id=@Id";
                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("Id", id, DbType.Int32);
                    parameters.Add("PasswordHash", passwordhash, DbType.String);
                    var result = await conn.ExecuteAsync(query, parameters);
                    if (result == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return await GetUserById(id);
                    }
                }
            }
        }

        

        public async Task<IEnumerable<FeatureFunction>> GetFeaturePermissionForRole(int roleid)
        {
            using (var conn = Connection)
            {
                var query = @"Select FF.Id,FF.display_name,UP.id,UP.access_level, UP.start_date,UP.end_date,UP.FeatureFunctionId, UP.RoleId
                                from FeatureFunctions FF inner join UserPermissions UP
                                On  FF.Id = UP.FeatureFunctionId 
                                Where UP.RoleId = @roleid";

                var parameters = new DynamicParameters();

                parameters.Add("RoleId", roleid, DbType.String);

                var features = await conn.QueryAsync<FeatureFunction, UserPermission, FeatureFunction>(query, (feature, permission) =>
                {
                    feature.Permission.Add(permission);
                    return feature;
                }, parameters, splitOn: "FeatureFunctionId");
                var result = features.GroupBy(u => u.Id).Select(g =>
                {
                    var groupedFeatures = g.First();
                    groupedFeatures.Permission = g.Select(u => u.Permission.Single()).ToList();
                    return groupedFeatures;
                });
                return result;
            }
        }


        public async Task<OrganisationDTO> GetOrganisationAddressbyId(int orgId)
        {
            using (var conn = Connection)
            {
                
                var parameters = new DynamicParameters();
                parameters.Add("orgId", orgId, DbType.Int64);

                var OrgAddress = await conn.QueryAsync<OrganisationDTO, Address, OrganisationDTO>("sp_GetOrganisationAddressbyId", (organisation,address) =>
                { 
                    organisation.Addresses.Add(address);
                    return organisation;
                 
                }, parameters,splitOn: "Addressid,OrganisationId", commandType: CommandType.StoredProcedure);
                var result = OrgAddress.GroupBy(u => u.Id).Select(g =>
                {
                    var groupedOrg = g.First();
                   groupedOrg.Addresses= g.Select(u => u.Addresses.Single()).ToList();
                    return groupedOrg;
                });

                return result.FirstOrDefault();
            }
        }

        public async Task<int> UpdateFirstTimeUserLogin(string email, bool val)
        {
            _logger.LogInformation("Getting UpdateFirstTimeUserLogin for the user {email} ", email);

            try
            {
                if (email != null)
                {
                    try
                    {
                        using (var conn = Connection)
                        {

                            var parameters = new DynamicParameters();
                            parameters.Add("@email", email, DbType.String);
                            parameters.Add("@changeFirstTimeUserValue", val, DbType.Boolean);
                            await conn.ExecuteAsync("sp_UpdateFirstTimeUser", parameters,commandType:CommandType.StoredProcedure);
                            //await conn.ExecuteAsync("Update AspNetUsers SET c_IsFirstTimeUser=0 WHERE email=@email",param:new{email=email});
                            return 1;

                        }
                    }
                    catch 
                    {
                        _logger.LogInformation("Exception thrown");
                        return 0;
                    }                    
                }
                else
                {
                    _logger.LogInformation("The input is not valid");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 0;
            }
        }

        public async Task<int> ResetUserLockout(string email)
        {
            _logger.LogInformation("ResetUserLockout for the user {email} ", email);

            try
            {
                if (email != null)
                {
                    try
                    {
                        using (var conn = Connection)
                        {

                            var parameters = new DynamicParameters();
                            parameters.Add("@email", email, DbType.String);
                            await conn.ExecuteAsync("sp_ResetUserLockout", parameters, commandType: CommandType.StoredProcedure);
                            return 1;

                        }
                    }
                    catch
                    {
                        _logger.LogInformation("Exception thrown");
                        return 0;
                    }
                }
                else
                {
                    _logger.LogInformation("The input is not valid");
                    return 0;
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