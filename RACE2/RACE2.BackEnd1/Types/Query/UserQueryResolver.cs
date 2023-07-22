using RACE2.DataModel;
using RACE2.Dto;
using RACE2.Notification;
using RACE2.Services;

namespace RACE2.BackEnd.Types
{
    [QueryType]
    public class UserQueryResolver
    {
        private readonly ILogger<UserQueryResolver> _logger;
        private readonly IConfiguration _configuration;

        public UserQueryResolver(ILogger<UserQueryResolver> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IEnumerable<UserDetail>> GetUserDetails(IUserService _userService)
        {
            try
            {
                _logger.LogInformation("GetUserDetailsReselvor");
                var result = await _userService.GetUserDetails();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }

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

        public async Task<UserSpecificDto> GetUserWithRoles(IUserService _userService, string email)
        {
            var result = await _userService.GetUserWithRoles(email);
            return result;
        }

        public async Task<UserDetail> MatchUserWithEmailAndPasswordHash(IUserService _userService, string email, string passwordhash)
        {
            var result = await _userService.MatchUserWithEmailAndPasswordHash(email, passwordhash);
            return result;
        }

        public async Task<IntegrationResponseModel> GetEngineerReservoirByUUID(IRACEIntegrationService _integrationService, string uuid)
        {
            var result = await _integrationService.GetEngineerReservoirByUUID(uuid);
            return result;
        }

        public async Task<OrganisationDTO> GetOrganisationAddressbyId(IUserService _userService, int orgId)
        {
            var result = await _userService.GetOrganisationAddressbyId(orgId);
            return result;
        }
    } 
}
