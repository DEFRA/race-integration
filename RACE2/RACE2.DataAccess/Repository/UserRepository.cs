
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
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Humanizer.In;

namespace RACE2.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {

        IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../appsettings.json").Build();
        }
        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        public async Task<IEnumerable<Userdetails>> GetUserdetails()
        {
            var query = "SELECT * FROM AspNetUsers";
            using (var conn = Connection)
            {
                var companies = await conn.QueryAsync<Userdetails>(query);
                return companies.ToList();
            }
        }

        public async Task<Userdetails> GetUserById(int id)
        {
            using (var conn = Connection)
            {
                var query = "Select * from AspNetUsers where Id=@id";
                var user = await conn.QuerySingleAsync<Userdetails>(query, new { Id = id });
                return user;
            }
        }


        public async Task<Userdetails> GetUserByEmailID(string email)
        {
            using (var conn = Connection)
            {
                var query = "Select * FROM AspNetUsers where Email = @email";
                var users = await conn.QuerySingleAsync<Userdetails>(query, new { email });
                return users;
            }
        }

        public async Task<Userdetails> GetUserWithRoles(string email)
        {
            using (var conn = Connection)
            {
                var query = @"Select A.Id, A.Email,A.UserName,B.UserId,B.RoleId,c.Id,c.Name
                              from AspNetUsers A inner join AspNetUserRoles B
                              ON  A.Id =b.UserId inner join AspNetRoles c
                              On c.Id = b.RoleId Where A.Email=@email";

                var parameters = new DynamicParameters();

                parameters.Add("Email", email, DbType.String);

                var users = await conn.QueryAsync<Userdetails, Roles, Userdetails>(query, (user, role) => {
                    user.Roles.Add(role);
                    return user;
                },parameters, splitOn: "RoleId");

                var result = users.GroupBy(u => u.Id).Select(g =>
                {
                    var groupedUser = g.First();
                    groupedUser.Roles = g.Select(u => u.Roles.Single()).ToList();
                    return groupedUser;
                });
                return result.FirstOrDefault();
            }
        }
        public async Task<Userdetails> CreateUser(Userdetails newuser)
        {
            var query = "INSERT INTO AspNetUsers (c_defra_id,c_type,c_display_name,c_first_name,c_last_name,c_status,c_created_on_date,c_last_access_date,c_password_retry_count,EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount) VALUES (@c_defra_id,@c_type,@c_display_name,@c_first_name,@c_last_name,@c_status,@c_created_on_date,@c_last_access_date,@c_password_retry_count,@EmailConfirmed,@PhoneNumberConfirmed,@TwoFactorEnabled,@LockoutEnabled,@AccessFailedCount)"
                + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var parameters = new DynamicParameters();
            //parameters.Add("Id", newuser.Id, DbType.String);
            parameters.Add("c_defra_id", newuser.c_defra_id, DbType.String);
            parameters.Add("c_type", newuser.c_type, DbType.String);
            parameters.Add("c_display_name", newuser.c_display_name, DbType.String);
            parameters.Add("c_first_name", newuser.c_first_name, DbType.String);
            parameters.Add("c_last_name", newuser.c_last_name, DbType.String);
            parameters.Add("c_status", newuser.c_status, DbType.String);
            parameters.Add("c_created_on_date", newuser.c_created_on_date, DbType.DateTime);
            parameters.Add("c_last_access_date", newuser.c_last_access_date, DbType.DateTime);
            parameters.Add("c_password_retry_count", newuser.c_password_retry_count, DbType.Int32);
            parameters.Add("EmailConfirmed", newuser.EmailConfirmed, DbType.Byte);
            parameters.Add("PhoneNumberConfirmed", newuser.PhoneNumberConfirmed, DbType.Byte);
            parameters.Add("TwoFactorEnabled", newuser.TwoFactorEnabled, DbType.Byte);
            parameters.Add("LockoutEnabled", newuser.LockoutEnabled, DbType.Byte);
            parameters.Add("AccessFailedCount", newuser.AccessFailedCount, DbType.Int32);

            using (var conn = Connection)
            {


                var id = await conn.QuerySingleAsync<int>(query, parameters);
                var createdCompany = new Userdetails
                {
                    Id = newuser.Id,
                    c_defra_id = newuser.c_defra_id,
                    c_type = newuser.c_type,
                    c_display_name = newuser.c_display_name,
                    c_first_name = newuser.c_first_name,
                    c_last_name = newuser.c_last_name,
                    c_status = newuser.c_status,
                    c_created_on_date = newuser.c_created_on_date,
                    c_last_access_date = newuser.c_last_access_date,
                    c_password_retry_count = newuser.c_password_retry_count,
                    EmailConfirmed = newuser.EmailConfirmed,
                    PhoneNumberConfirmed = newuser.PhoneNumberConfirmed,
                    TwoFactorEnabled = newuser.TwoFactorEnabled,
                    LockoutEnabled = newuser.LockoutEnabled,
                    AccessFailedCount = newuser.AccessFailedCount

                };
                return createdCompany;
            }
        }

        public async Task<Userdetails> ValidateUser(Userdetails loginuser)
        {
            using (var conn = Connection)
            {
                var res = await GetUserByEmailID(loginuser.Email);
                if (res == null)
                {
                    var query = "INSERT INTO AspNetUsers (c_defra_id,c_type,c_display_name,c_first_name,c_last_name,c_mobile,c_emergency_phone,c_organisation_id,c_organisation_name,c_job_title,c_current_panel,c_paon,c_saon,c_status,c_created_on_date,c_last_access_date,c_password_retry_count,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount) VALUES (@c_defra_id,@c_type,@c_display_name,@c_first_name,@c_last_name,@c_mobile,@c_emergency_phone,@c_organisation_id,@c_organisation_name,@c_job_title,@c_current_panel,@c_paon,@c_saon,@c_status,@c_created_on_date,@c_last_access_date,@c_password_retry_count,@UserName,@NormalizedUserName,@Email,@NormalizedEmail,@EmailConfirmed,@PhoneNumber,@PhoneNumberConfirmed,@TwoFactorEnabled,@LockoutEnabled,@AccessFailedCount)"
                + "SELECT CAST(SCOPE_IDENTITY() as int)";
                    var parameters = new DynamicParameters();
                    //parameters.Add("Id", newuser.Id, DbType.String);
                    parameters.Add("c_defra_id", loginuser.c_defra_id, DbType.String);
                    parameters.Add("c_type", loginuser.c_type, DbType.String);
                    parameters.Add("c_display_name", loginuser.c_display_name, DbType.String);
                    parameters.Add("c_first_name", loginuser.c_first_name, DbType.String);
                    parameters.Add("c_last_name", loginuser.c_last_name, DbType.String);
                    parameters.Add("c_mobile", loginuser.c_mobile, DbType.String);
                    parameters.Add("c_emergency_phone", loginuser.c_emergency_phone, DbType.String);
                    parameters.Add("c_organisation_id", loginuser.c_organisation_id, DbType.String);
                    parameters.Add("c_organisation_name", loginuser.c_organisation_name, DbType.String);
                    parameters.Add("c_job_title", loginuser.c_job_title, DbType.String);
                    parameters.Add("c_current_panel", loginuser.c_current_panel, DbType.String);
                    parameters.Add("c_paon", loginuser.c_paon, DbType.String);
                    parameters.Add("c_saon", loginuser.c_saon, DbType.String);
                    parameters.Add("c_status", loginuser.c_status, DbType.String);
                    parameters.Add("c_created_on_date", loginuser.c_created_on_date, DbType.DateTime);
                    parameters.Add("c_last_access_date", loginuser.c_last_access_date, DbType.DateTime);
                    parameters.Add("c_password_retry_count", loginuser.c_password_retry_count, DbType.Int32);
                    parameters.Add("UserName", loginuser.UserName, DbType.String);
                    parameters.Add("NormalizedUserName", loginuser.NormalizedUserName, DbType.String);
                    parameters.Add("Email", loginuser.Email, DbType.String);
                    parameters.Add("NormalizedEmail", loginuser.NormalizedEmail, DbType.String);
                    parameters.Add("EmailConfirmed", loginuser.EmailConfirmed, DbType.Byte);
                    parameters.Add("PhoneNumber", loginuser.PhoneNumber, DbType.String);
                    parameters.Add("PhoneNumberConfirmed", loginuser.PhoneNumberConfirmed, DbType.Byte);
                    parameters.Add("TwoFactorEnabled", loginuser.TwoFactorEnabled, DbType.Byte);
                    parameters.Add("LockoutEnabled", loginuser.LockoutEnabled, DbType.Byte);
                    parameters.Add("AccessFailedCount", loginuser.AccessFailedCount, DbType.Int32);

                    int id = await conn.ExecuteScalarAsync<int>(query, loginuser);
                    return await GetUserById(id);

                }
                else
                {
                    var query = @"UPDATE AspNetUsers
                                SET c_defra_id=@c_defra_id,c_type=@c_type,c_display_name=@c_display_name,
                                    c_first_name=@c_first_name,c_last_name=@c_last_name,c_mobile=@c_mobile,c_emergency_phone=@c_emergency_phone,
                                    c_organisation_id=@c_organisation_id,c_organisation_name=@c_organisation_name,c_job_title=@c_job_title,
                                    c_current_panel=@c_current_panel,c_paon=@c_paon,c_saon=@c_saon,c_status=@c_status,
                                    c_created_on_date=@c_created_on_date,c_last_access_date=@c_last_access_date,
                                    c_password_retry_count=@c_password_retry_count,UserName=@UserName,NormalizedUserName=@NormalizedUserName,
                                    Email=@Email,NormalizedEmail=@NormalizedEmail,EmailConfirmed=@EmailConfirmed,PhoneNumber=@PhoneNumber,PhoneNumberConfirmed=@PhoneNumberConfirmed,TwoFactorEnabled=@TwoFactorEnabled,LockoutEnabled=@LockoutEnabled,AccessFailedCount=@AccessFailedCount
                                WHERE Id=@Id";


                    var parameters = new DynamicParameters();
                    parameters.Add("Id", res.Id, DbType.String);
                    parameters.Add("c_defra_id", loginuser.c_defra_id, DbType.String);
                    parameters.Add("c_type", loginuser.c_type, DbType.String);
                    parameters.Add("c_display_name", loginuser.c_display_name, DbType.String);
                    parameters.Add("c_first_name", loginuser.c_first_name, DbType.String);
                    parameters.Add("c_last_name", loginuser.c_last_name, DbType.String);
                    parameters.Add("c_mobile", loginuser.c_mobile, DbType.String);
                    parameters.Add("c_emergency_phone", loginuser.c_emergency_phone, DbType.String);
                    parameters.Add("c_organisation_id", loginuser.c_organisation_id, DbType.String);
                    parameters.Add("c_organisation_name", loginuser.c_organisation_name, DbType.String);
                    parameters.Add("c_job_title", loginuser.c_job_title, DbType.String);
                    parameters.Add("c_current_panel", loginuser.c_current_panel, DbType.String);
                    parameters.Add("c_paon", loginuser.c_paon, DbType.String);
                    parameters.Add("c_saon", loginuser.c_saon, DbType.String);
                    parameters.Add("c_status", loginuser.c_status, DbType.String);
                    parameters.Add("c_created_on_date", loginuser.c_created_on_date, DbType.DateTime);
                    parameters.Add("c_last_access_date", loginuser.c_last_access_date, DbType.DateTime);
                    parameters.Add("c_password_retry_count", loginuser.c_password_retry_count, DbType.Int32);
                    parameters.Add("UserName", loginuser.UserName, DbType.String);
                    parameters.Add("NormalizedUserName", loginuser.NormalizedUserName, DbType.String);
                    parameters.Add("Email", loginuser.Email, DbType.String);
                    parameters.Add("NormalizedEmail", loginuser.NormalizedEmail, DbType.String);
                    parameters.Add("EmailConfirmed", loginuser.EmailConfirmed, DbType.Byte);
                    parameters.Add("PhoneNumber", loginuser.PhoneNumber, DbType.String);
                    parameters.Add("PhoneNumberConfirmed", loginuser.PhoneNumberConfirmed, DbType.Byte);
                    parameters.Add("TwoFactorEnabled", loginuser.TwoFactorEnabled, DbType.Byte);
                    parameters.Add("LockoutEnabled", loginuser.LockoutEnabled, DbType.Byte);
                    parameters.Add("AccessFailedCount", loginuser.AccessFailedCount, DbType.Int32);

                    await conn.ExecuteAsync(query, parameters);
                    return await GetUserById(loginuser.Id);
                }
            }
        }



    }
}
