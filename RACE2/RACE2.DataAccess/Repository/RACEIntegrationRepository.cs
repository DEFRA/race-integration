using Newtonsoft.Json;
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
            string baseuri = "https://eadev.synapps-solutions.com/integration-poc/";
            try
            {
              

                IntegrationPayLoadModel model = new IntegrationPayLoadModel
                {
                    uuid = "0801117180006e9b",
                    email = "edmund.engineer@eadev.synapps-solutions.com"
                };
                //ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                string body = JsonConvert.SerializeObject(model);
                var options = new RestClientOptions(baseuri)
                {
                    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                };
                RestClient client = new RestClient(options);
                var request = new RestRequest("search/engineer-reservoir-by-uuid", Method.Post);
                request.RequestFormat = DataFormat.Json;
                // request.AddJsonBody( new {uuid = "0801117180006e9b", email = "edmund.engineer@eadev.synapps-solutions.com" });
                request.AddJsonBody("{\r\n    \"engineer_reservoir_search\" : {\r\n        \"uuid\" : \"0801117180006e9b\",\r\n        \"email\" : \"edmund.engineer@eadev.synapps-solutions.com\"\r\n    }\r\n}");

                //  request.AddParameter("application/json", body, ParameterType.RequestBody);
                RestResponse response = await client.ExecuteAsync(request);
                var stringOutput = JsonConvert.DeserializeObject<dynamic>(response.Content);

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
