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
        public IUserService _userService;
        public UserResolver(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IEnumerable<Userdetails>> GetUserdetails()
        {
            return await _userService.GetUserdetails();
        }

        public async Task<Userdetails> GetById(int id)
        {
            return await _userService.GetUserById(id);
        }

        public async Task<Userdetails> GetUserByEmailID(string email)
        {
            return await _userService.GetUserByEmailID(email);
        }

      
    }
}
