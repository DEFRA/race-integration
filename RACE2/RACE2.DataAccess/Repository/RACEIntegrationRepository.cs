using RACE2.Dto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataAccess.Repository
{
    public class RACEIntegrationRepository : IRACEIntegrationRepository
    {
        public async Task<IntegrationResponseModel> GetEngineerReservoirByUUID(string uuid, string email)
        {
            string baseuri = "https://eadev.synapps-solutions.com/integration-poc/search/engineer-reservoir-by-uuid";
            try
            {
                RestClient client = new RestClient(baseuri);


                IntegrationResponseModel integrationResponseModel = new IntegrationResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Status = "Failed",
                    Reason = "Exception while retrieving the engineer details",
                    ResponseData = null
                };
                return integrationResponseModel;

            }
            catch
            {
                IntegrationResponseModel integrationResponseModel = new IntegrationResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Status = "Failed",
                    Reason = "Exception while retrieving the engineer details",
                    ResponseData = null
                };
                return integrationResponseModel;

            }

        }
    }
}
