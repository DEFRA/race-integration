using HotChocolate.Subscriptions;
using RACE2.DataAccess;
using RACE2.DataModel;
using RACE2.Services;

namespace RACE2.BackEnd.Mutation
{
    [MutationType]
    public class UserMutationResolver
    {
        public async Task<UserDetail> CreateUser([Service] IUserService _userService, UserDetail newuser)
        {
            var result = await _userService.CreateUser(newuser);
            return result;
        }

        public async Task<UserDetail> ValidateUser([Service] IUserService _userService, UserDetail loginuser)
        {
            var result = await _userService.ValidateUser(loginuser);
            return result;
        }

        public async Task<UserDetail> UpdatePasswordHashForUser([Service] IUserService _userService, int id, string passwordhash)
        {
            var result = await _userService.UpdatePasswordHashForUser(id, passwordhash);
            return result;
        }
    }
}
