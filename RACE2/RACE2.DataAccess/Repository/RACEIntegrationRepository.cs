using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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

        private readonly ILogger<RACEIntegrationRepository> _logger;
        private readonly IConfiguration _configuration;

        public RACEIntegrationRepository(IConfiguration configuration, ILogger<RACEIntegrationRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<IntegrationResponseModel> GetEngineerReservoirByUUID(string uuid)
        {
            string baseuri = "https://eadev.synapps-solutions.com/integration-poc/";
            try
            {
              

                IntegrationPayLoadModel model = new IntegrationPayLoadModel
                {
                    uuid = uuid, //"0801117180006e9b",
                    email = "edmund.engineer@eadev.synapps-solutions.com"
                };
                PayloadModel modelbody = new PayloadModel
                {
                    engineer_reservoir_search = model
                };
               
                string body = JsonConvert.SerializeObject(modelbody);
                var options = new RestClientOptions(baseuri)
                {
                    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                };
                RestClient client = new RestClient(options);
                var request = new RestRequest("search/engineer-reservoir-by-uuid", Method.Post);
                request.RequestFormat = DataFormat.Json;
                // request.AddJsonBody( new {uuid = "0801117180006e9b", email = "edmund.engineer@eadev.synapps-solutions.com" });
                // request.AddJsonBody("{\r\n    \"engineer_reservoir_search\" : {\r\n        \"uuid\" : \"0801117180006e9b\",\r\n        \"email\" : \"edmund.engineer@eadev.synapps-solutions.com\"\r\n    }\r\n}");
                request.AddJsonBody(body);
                //  request.AddParameter("application/json", body, ParameterType.RequestBody);
                RestResponse response = await client.ExecuteAsync(request);
                var stringOutput = JsonConvert.DeserializeObject<dynamic>(response.Content);

                IntegrationResponseModel integrationResponseModel = new IntegrationResponseModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Status = "Success",
                    Reason = "Success",
                    ResponseData = stringOutput.ToString()
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


        public async Task<IntegrationResponseModel> SubmitDocumentToBackend(AnnualSubmissionDocumentDetails submitS12Statement)
        {

            IntegrationResponseModel integrationResponseModel = new IntegrationResponseModel();
            string baseuri = "https://eadev.synapps-solutions.com/EA-API/";
            _logger.LogInformation("Entering the document details");
            try
            {
                string body = JsonConvert.SerializeObject(submitS12Statement);
                var options = new RestClientOptions(baseuri)
                {
                    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                };
                RestClient client = new RestClient(options);
                var request = new RestRequest("submission", Method.Post);
                request.AddHeader("RACE_REST_API_KEY", _configuration["DocumentumAPIKey"]);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(body);
                RestResponse response = await client.ExecuteAsync(request);
                //var stringOutput = JsonConvert.DeserializeObject<dynamic>(response.Content);
                integrationResponseModel.StatusCode = response.StatusCode;
                integrationResponseModel.Status = response.StatusDescription;
                integrationResponseModel.Reason = response.ErrorMessage;
                integrationResponseModel.ResponseData = response.Content.ToString();
                return integrationResponseModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                integrationResponseModel.StatusCode = System.Net.HttpStatusCode.BadRequest;
                integrationResponseModel.Status = "Bad Request";
                integrationResponseModel.Reason = ex.Message;
                return integrationResponseModel;
            }

        }
    }
}
