
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

namespace RACE2.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Userdetails>> GetUserdetails()
        {
            var query = "SELECT * FROM AspNetUsers";
            using (var connection = _applicationDbContext.Database.GetDbConnection())
            {
                var companies = await connection.QueryAsync<Userdetails>(query);
                return companies.ToList();
            }
        }

        public async Task CreateUser(Userdetails newuser)
        {
            var query = "INSERT INTO AspNetUsers (Id,c_defra_id,c_type,c_display_name,c_first_name,c_last_name,c_status,c_created_on_date,c_last_access_date,c_password_retry_count,EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount) VALUES (@, @Address, @Country)";
            var parameters = new DynamicParameters();
          ////  parameters.Add("Name", company.Name, DbType.String);
          // // parameters.Add("Address", company.Address, DbType.String);
          //  parameters.Add("Country", company.Country, DbType.String);
          //  using (var connection = _context.CreateConnection())
          //  {
          //      await connection.ExecuteAsync(query, parameters);
          //  }
        }

    }
}

