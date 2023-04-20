using RACE2.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Services
{
    public interface IRACEIntegrationService
    {
        public Task<IntegrationResponseModel> GetEngineerReservoirByUUID(string uuid);
    }
}
