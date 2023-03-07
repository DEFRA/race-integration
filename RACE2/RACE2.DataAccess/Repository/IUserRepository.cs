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
        public Task<IEnumerable<UserDetail>> GetUserDetails();
        public Task<UserDetail> GetUserById(int id);
        public Task<UserDetail> GetUserByEmailID(string email);
        public Task<UserDetail> CreateUser(UserDetail newuser);
        public Task<UserDetail> ValidateUser(UserDetail loginuser);
        public Task<UserDetail> GetUserWithRoles(string email);
        public Task<UserDetail> MatchUserWithEmailAndPasswordHash(string email, string passwordhash);
        public Task<UserDetail> UpdatePasswordHashForUser(int id, string passwordhash);      

        public Task<IEnumerable<FeatureFunction>> GetFeaturePermissionForRole(int roleid);

        public Task<UserDetail> GetReservoirsByUserId(int id);
        public Task<UserDetail> GetReservoirsByUserEmailId(string email);

    }
}
