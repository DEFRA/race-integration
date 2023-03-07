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
        public Task<IEnumerable<UserDetail>> GetUserDetails();
        public Task<UserDetail> GetUserById(int id);
        public Task<UserDetail> GetUserByEmailID(string email);
        public Task<UserDetail> CreateUser(UserDetail newuser);
        public Task<UserDetail> ValidateUser(UserDetail loginuser);
        public Task<UserDetail> GetUserWithRoles(string email);
        public Task<UserDetail> MatchUserWithEmailAndPasswordHash(string email, string passwordhash);
        public Task<UserDetail> UpdatePasswordHashForUser(int id, string passwordhash);
        public Task<UserDetail> GetReservoirsByUserId(int id);
        public Task<UserDetail> GetReservoirsByUserEmailId(string email);
        public Task<IEnumerable<FeatureFunction>> GetFeaturePermissionForRole(int roleid);
    }
}
