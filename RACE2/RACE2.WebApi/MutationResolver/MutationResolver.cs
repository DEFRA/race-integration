using HotChocolate.Subscriptions;
using RACE2.DataAccess;
using RACE2.DataModel;
using RACE2.Services;

namespace RACE2.WebApi.MutationResolver
{
    public class MutationResolver
    {
        public async Task<Userdetails> CreateUser(IUserService _userService, Userdetails newuser)
        {
            var result = await _userService.CreateUser(newuser);
            return result;
        }

        public async Task<Userdetails> ValidateUser(IUserService _userService, Userdetails loginuser)
        {
            var result = await _userService.ValidateUser(loginuser);
            return result;
        }
    }
}
