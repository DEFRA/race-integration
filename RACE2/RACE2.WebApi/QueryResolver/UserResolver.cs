using HotChocolate.Resolvers;
using HotChocolate.Subscriptions;
using RACE2.DataAccess;
using RACE2.DataAccess.Repository;
using RACE2.DataModel;
using RACE2.Services;

namespace RACE2.WebApi.QueryResolver
{
    public class UserResolver
    {
        public async Task<IEnumerable<Userdetails>> GetUserdetails(IUserService _userService)
        {
            return await _userService.GetUserdetails();
        }

        public async Task<Userdetails> GetById(IUserService _userService, int id)
        {
            return await _userService.GetUserById(id);
        }

        public async Task<Userdetails> GetUserByEmailID(IUserService _userService, string email)
        {
            return await _userService.GetUserByEmailID(email);
        }

        public async Task<Userdetails> GetUsersWithRoles(IUserService _userService,string email)
        {
            var result=  await _userService.GetUsersWithRoles(email);
            return result;
        }
    }
}
