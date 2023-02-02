
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

        public async Task<Userdetails> CreateUser(Userdetails newuser)
        {
            var query = "INSERT INTO AspNetUsers (Id,c_defra_id,c_type,c_display_name,c_first_name,c_last_name,c_status,c_created_on_date,c_last_access_date,c_password_retry_count,EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount) VALUES (@Id,@c_defra_id,@c_type,@c_display_name,@c_first_name,@c_last_name,@c_status,@c_created_on_date,@c_last_access_date,@c_password_retry_count,@EmailConfirmed,@PhoneNumberConfirmed,@TwoFactorEnabled,@LockoutEnabled,@AccessFailedCount)"
                + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var parameters = new DynamicParameters();
            parameters.Add("Id", newuser.Id, DbType.String);
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
                //await conn.ExecuteAsync(query, parameters);

                var id = await conn.QuerySingleAsync<int>(query, parameters);
                var createdCompany = new Userdetails
                {
                    Id = newuser.Id,
                    c_defra_id= newuser.c_defra_id,
                    c_type = newuser.c_type,
                    c_display_name= newuser.c_display_name,
                    c_first_name= newuser.c_first_name,
                    c_last_name= newuser.c_last_name,
                    c_status= newuser.c_status,
                    c_created_on_date=newuser.c_created_on_date,
                    c_last_access_date=newuser.c_last_access_date,
                    c_password_retry_count =newuser.c_password_retry_count,
                    EmailConfirmed=newuser.EmailConfirmed,
                    PhoneNumberConfirmed=newuser.PhoneNumberConfirmed,
                    TwoFactorEnabled=newuser.TwoFactorEnabled,
                    LockoutEnabled=newuser.LockoutEnabled,
                    AccessFailedCount=newuser.AccessFailedCount                        
                   
                };
                return createdCompany;
            }
        }

    }
}

