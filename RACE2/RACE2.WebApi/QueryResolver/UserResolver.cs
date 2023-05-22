using HotChocolate.Resolvers;
using HotChocolate.Subscriptions;
using RACE2.DataAccess;
using RACE2.DataAccess.Repository;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.Notification;
using RACE2.Services;
using Serilog;
using Serilog.Events;

namespace RACE2.WebApi.QueryResolver
{
    public class UserResolver
    {
        
       
        public async Task<IEnumerable<UserDetail>> GetUserDetails(IUserService _userService)
        {
            return await _userService.GetUserDetails();
        }

        public async Task<UserDetail> GetById(IUserService _userService, int id)
        {
            return await _userService.GetUserById(id);
        }

        public async Task<UserSpecificDto> GetUserByEmailID(IUserService _userService, string email)
        {
            RaceNotification raceNotification = new RaceNotification();
            //await raceNotification.SendForgotPasswordMail("mahalakshmi.alagarsamy@capgemini.com", "Maha", "https://race2securityprovider.gentlebush-defe7f09.westeurope.azurecontainerapps.io/Identity/Account/ForgotPassword");
            //await raceNotification.SendMail(email);
            //await raceNotification.SendEmailTestWithPersonalisation(email);

            return await _userService.GetUserByEmailID(email);     
            
        }

        public async Task<UserSpecificDto> GetUserWithRoles(IUserService _userService,string email)
        {
            var result=  await _userService.GetUserWithRoles(email);
            return result;
        }

        public async Task<UserDetail> MatchUserWithEmailAndPasswordHash(IUserService _userService, string email, string passwordhash)
        {
            var result = await _userService.MatchUserWithEmailAndPasswordHash(email,passwordhash);
            return result;
        }

        public async Task<List<ReservoirDetailsDTO>> GetReservoirsByUserId(IUserService _userService, int id)
        {
            var result = await _userService.GetReservoirsByUserId(id);
            return result;
        }

        public async Task<List<ReservoirDetailsDTO>> GetReservoirsByUserEmailId(IUserService _userService, string emailId)
        {
            var result = await _userService.GetReservoirsByUserEmailId(emailId);
            return result;
        }

        public async Task<IEnumerable<FeatureFunction>> GetFeaturePermissionForRole(IUserService _userService,int roleid)
        {
            var result = await _userService.GetFeaturePermissionForRole(roleid);
            return result;
        }

        public async Task<IntegrationResponseModel> GetEngineerReservoirByUUID(IRACEIntegrationService _integrationService, string uuid)
        {
            var result = await _integrationService.GetEngineerReservoirByUUID(uuid);
            return result;
        }


        public async Task<Organisation> GetOrganisationByUserId(IUserService _userService, int userId)
        {
           var result = await _userService.GetOrganisationAddressbyId(userId);
            return result;
        }

        public async Task<List<DataModel.Action>> GetActionsListByReservoirIdAndCategory(IUserService _userService, int reservoirid, int category)
        {
            return await _userService.GetActionsListByReservoirIdAndCategory(reservoirid,category);
        }

        public async Task<List<SafetyMeasure>> GetSafetyMeasuresListByReservoirId(IUserService _userService, int reservoirid)
        {
            return await _userService.GetSafetyMeasuresListByReservoirId(reservoirid);
        }
    }
}
