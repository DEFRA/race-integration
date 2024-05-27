using RACE2.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RACE2.DataAccess.Repository
{
    public interface IRACEIntegrationRepository
    {
        public Task<IntegrationResponseModel> GetEngineerReservoirByUUID(string uuid);

        public Task<IntegrationResponseModel> SubmitDocumentToBackend(AnnualSubmissionDocumentDetails submitWrittenStatement);
    }

}
