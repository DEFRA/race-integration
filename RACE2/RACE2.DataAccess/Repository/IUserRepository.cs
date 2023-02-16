using RACE2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RACE2.DataAccess.Repository
{
    public interface IUserRepository
    {
        public Task<IEnumerable<Userdetails>> GetUserdetails();
        public Task<Userdetails> GetUserById(int id);
        public Task<Userdetails> GetUserByEmailID(string email);
        public Task<Userdetails> CreateUser(Userdetails newuser);
        public Task<Userdetails> ValidateUser(Userdetails loginuser);

        public Task<Userdetails> GetUsersWithRoles(string email);
    }
}
