using RACE2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDetail>> GetUserDetail();
        public Task<UserDetail> GetUserById(int id);
        public Task<UserDetail> GetUserByEmailID(string email);
        public Task<UserDetail> CreateUser(UserDetail newuser);
        public Task<UserDetail> ValidateUser(UserDetail loginuser);
        public Task<UserDetail> GetUserWithRoles(string email);
    }
}
