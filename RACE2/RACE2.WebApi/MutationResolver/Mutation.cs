using HotChocolate.Subscriptions;
using RACE2.DataAccess;
using RACE2.DataModel;
using RACE2.Services;

namespace RACE2.WebApi.MutationResolver
{
    public class Mutation
    {
        public IUserService _userService;
        public Mutation(IUserService userService)
        {
            _userService = userService;
        }


        public async Task<Userdetails> CreateUser(Userdetails newuser)
        {
            var result = await _userService.CreateUser(newuser);
            return result;
        }

        public async Task<Userdetails> ValidateUser(Userdetails loginuser)
        {
            var result = await _userService.ValidateUser(loginuser);
            return result;
        }
    }
}
