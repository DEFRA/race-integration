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
        public Task<IEnumerable<Userdetail>> GetUserdetail();
        public Task<Userdetail> GetUserById(int id);
        public Task<Userdetail> GetUserByEmailID(string email);
        public Task<Userdetail> CreateUser(Userdetail newuser);
        public Task<Userdetail> ValidateUser(Userdetail loginuser);

        public Task<Userdetail> GetUserWithRoles(string email);
    }
}
