using HotChocolate.Subscriptions;
using RACE2.DataAccess;
using RACE2.DataModel;
using RACE2.Services;

namespace RACE2.WebApi.MutationResolver
{
    public class MutationResolver
    {
        public async Task<Userdetail> CreateUser(IUserService _userService, Userdetail newuser)
        {
            var result = await _userService.CreateUser(newuser);
            return result;
        }

        public async Task<Userdetail> ValidateUser(IUserService _userService, Userdetail loginuser)
        {
            var result = await _userService.ValidateUser(loginuser);
            return result;
        }
    }
}
