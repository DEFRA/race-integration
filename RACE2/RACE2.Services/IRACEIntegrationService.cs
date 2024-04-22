using RACE2.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RACE2.Dto.DocumentDetails;

namespace RACE2.Services
{
    public interface IRACEIntegrationService
    {
        public Task<IntegrationResponseModel> GetEngineerReservoirByUUID(string uuid);
        public Task<IntegrationResponseModel> SubmitDocumentToBackend(SubmitS12Statement submitS12Statement);
    }
}
