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
           // int noOfRecs = await _cakeService.SaveCake(newCake);
            //var result = await _cakeService.GetAll();
            return result;
        }

        //public async Task<Cake> UpdateCakeAsync(Cake updatedCake)
        //{
        //    return await _cakeService.UpdateCake(updatedCake);
        //}

        //public async Task<List<Cake>> DeleteCakeAsync(int id)
        //{
        //    int result = await _cakeService.DeleteCake(id);
        //    return await _cakeService.GetAll();
        //}
    }
}
