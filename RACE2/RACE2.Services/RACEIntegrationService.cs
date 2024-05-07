using RACE2.DataAccess.Repository;
using RACE2.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RACE2.Services
{
    public class RACEIntegrationService :IRACEIntegrationService
    {
        public IRACEIntegrationRepository _IntegrationRepository;
        public RACEIntegrationService(IRACEIntegrationRepository integrationRepository)
        {
            _IntegrationRepository = integrationRepository;
        }
        public async Task<IntegrationResponseModel> GetEngineerReservoirByUUID(string uuid)
        {
            return await _IntegrationRepository.GetEngineerReservoirByUUID(uuid);
        }

        public async Task<IntegrationResponseModel> SubmitDocumentToBackend(AnnualSubmissionDocumentDetails submitS12Statement)
        {
            try
            {
                return await _IntegrationRepository.SubmitDocumentToBackend(submitS12Statement);
            }
            catch
            {
                throw;
            }
        }
    }
}
