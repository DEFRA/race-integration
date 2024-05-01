using RACE2.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RACE2.Dto.DocumentDetails;

namespace RACE2.DataAccess.Repository
{
    public interface IRACEIntegrationRepository
    {
        public Task<IntegrationResponseModel> GetEngineerReservoirByUUID(string uuid);

        public Task<IntegrationResponseModel> SubmitDocumentToBackend(SubmitS12Statement submitWrittenStatement);
    }

}
