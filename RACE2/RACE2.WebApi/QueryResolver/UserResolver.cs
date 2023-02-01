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
        public IUserRepository _userRepository;
        public UserResolver(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<Userdetails>> GetUserdetails()
        {
            return await _userService.GetUserdetails();
        }

        //public async Task<Cake> GetCakeById(int id)
        //{
        //    return await _cakeService.GetById(id);
        //}

        //public async Task<List<Cake>> GetCakeByName(string name)
        //{
        //    return await _cakeService.FilterByName(name);
        //}

    }
}
