using RACE2.DataModel;
using RACE2.Dto;
using RACE2.Notification;
using RACE2.Services;

namespace RACE2.BackEnd.Types
{
    [QueryType]
    public class UserResolver
    {
        private readonly ILogger<UserResolver> _logger;

        public UserResolver(ILogger<UserResolver> logger)
        {
            _logger = logger;
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

        public async Task<List<DataModel.Action>> GetActionsListByReservoirIdAndCategory(IUserService _userService, int reservoirid, int category)
        {
            return await _userService.GetActionsListByReservoirIdAndCategory(reservoirid, category);
        }

        public async Task<List<SafetyMeasure>> GetSafetyMeasuresListByReservoirId(IUserService _userService, int reservoirid)
        {
            return await _userService.GetSafetyMeasuresListByReservoirId(reservoirid);
        }

        public async Task<Address> GetAddressByReservoirId(IUserService _userService, int reservoirid, string operatortype)
        {
            try
            {
                _logger.LogInformation("calling GetAddressByReservoirIdReselvor");
                return await _userService.GetAddressByReservoirId(reservoirid, operatortype);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }

        }

        public async Task<List<OperatorDTO>> GetOperatorsforReservoir(IUserService _userService, int reservoirid, string operatortype)
        {
            try
            {
                _logger.LogInformation("calling GetOperatorsforReservoir");
                return await _userService.GetOperatorsforReservoir(reservoirid, operatortype);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<List<SubmissionStatusDTO>> GetReservoirStatusByEmail(IUserService _userService, string email)
        {
            try
            {
                _logger.LogInformation("calling GetReservoirStatusByEmail");
                return await _userService.GetReservoirStatusByEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
