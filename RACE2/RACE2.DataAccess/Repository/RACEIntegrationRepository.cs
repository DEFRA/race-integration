using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Server;

using RACE2.Dto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;




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
       

        public async Task<IntegrationResponseModel> SubmitDocumentToBackend(AnnualSubmissionDocumentDetails submitStatement)
        {

            IntegrationResponseModel integrationResponseModel = new IntegrationResponseModel();
            string baseuri = _configuration["DocumentumURL"];
            _logger.LogInformation("Entering the document details");
            try
            {
                //JsonSerializerOptions jsonoptions = new()
                //{
                //    DefaultIgnoreCondition = JsonIgnoreCondition.Always
                //};

                string uploadPayloadJson =
                    System.Text.Json.JsonSerializer.Serialize(submitStatement, new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition =JsonIgnoreCondition.WhenWritingNull
                    });

                //string body = JsonConvert.SerializeObject(submitS12Statement);
                var options = new RestClientOptions(baseuri)
                {
                    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                };
                RestClient client = new RestClient(options);
                var request = new RestRequest("submission", Method.Post);
                request.AddHeader("RACE_REST_API_KEY", _configuration["DocumentumAPIKey"]);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(uploadPayloadJson);
                RestResponse response = await client.ExecuteAsync(request);
                //var stringOutput = JsonConvert.DeserializeObject<dynamic>(response.Content);
                integrationResponseModel.StatusCode = response.StatusCode;
                integrationResponseModel.Status = response.StatusDescription;
                integrationResponseModel.Reason = response.ErrorMessage;
                integrationResponseModel.ResponseData = response.Content != null? response.Content.ToString(): "";
                _logger.LogInformation("Documentum Status code" + integrationResponseModel.StatusCode);
                _logger.LogInformation("Documentum Response" + integrationResponseModel.ResponseData);
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

        public AnnualSubmissionDocumentDetails GenerateSubmitPayloadJSON(int submittedBy, string submissionreference,string notificationemailaddress,string reservoirbackendid, 
            string reservoirreferencenumber,Stream filestream,int documentid,string uploadfilename,string blobstoragefilename, int engineerid,string backendprimaryref,string backendsecondref)
        {
            try
             {
                AnnualSubmissionDocumentDetails uploadPayload = new AnnualSubmissionDocumentDetails();
                string[] emailarray;
                if (!string.IsNullOrEmpty(notificationemailaddress))
                {
                    emailarray = notificationemailaddress.Split(';');
                    uploadPayload.writtenStatement.notificationEmailAddresses = emailarray;
                }

                _logger.LogInformation("Adding Submission Payload");
                
                //submission detaile
                uploadPayload.submission.statusId = 1;
                uploadPayload.submission.reference = submissionreference;
                string format = "yyyy-MM-ddTHH:mm:ss.fffzz";//"yyyy-MM-dd'T'HH:mm:ss.SSS'Z'";//"yyyy-MM-dd'T'HH:mm:ss";
                //string strDate = DateTime.Now.ToString("o"); 
                string strDate = DateTime.UtcNow.ToString(format, DateTimeFormatInfo.InvariantInfo);
                uploadPayload.submission.submittedDate = strDate + "00";//"2024-06-10T14:23:18.000+0000"; //strDate
                uploadPayload.submission.submittedBy = submittedBy;
                uploadPayload.submission.type = "Annual Statement";
                uploadPayload.submission.source = "S12 Digital Service";

                _logger.LogInformation("Adding Written Statement Payload");
                //written statement details
                uploadPayload.writtenStatement.type = "12(2) Written statement";
                uploadPayload.writtenStatement.date = null;
                uploadPayload.writtenStatement.visualInspectionDirection = true;
                uploadPayload.writtenStatement.recommendInspectionS10 = true;
                uploadPayload.writtenStatement.nextInspectionDate = null;
                uploadPayload.writtenStatement.expectedNextStatementDate = null;
                //uploadPayload.writtenStatement.notificationEmailAddresses = emailarray;

                _logger.LogInformation("Adding Reservoir Details Payload");
                //reservoir details
                uploadPayload.reservoir.backEndId = reservoirbackendid;//"0801117180035e68";
                uploadPayload.reservoir.referenceNumber = reservoirreferencenumber;

                //engineer details
                _logger.LogInformation("Adding Engineer Details Payload");
                uploadPayload.engineer.id = engineerid;
                uploadPayload.engineer.backEndPrimaryReference = backendprimaryref;// "08011171800069ee"; //backendprimaryref;
                uploadPayload.engineer.backEndSecondaryReference = backendsecondref;// "08011171800069ef";// ;

                //breach details

                _logger.LogInformation("Adding Document details Payload");
                //document details  
                uploadPayload.document.id = documentid;
                uploadPayload.document.uploadFileName = uploadfilename;
                if (!uploadfilename.IsNullOrEmpty())
                {
                    var fileExtns = uploadfilename.Split('.');
                    int totalExtns = fileExtns.Length;
                    var fileExtn = uploadfilename.Split('.')[totalExtns - 1];
                    if (fileExtn == "docx")
                        uploadPayload.document.mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    if (fileExtn == "doc")
                        uploadPayload.document.mimeType = "application/msword";
                    if (fileExtn == "pdf")
                        uploadPayload.document.mimeType = "application/pdf";
                }

                uploadPayload.document.protectiveMarking = "Official Sensitive";
                uploadPayload.document.templateType = "";
                uploadPayload.document.templateVersion = "";
                uploadPayload.document.blobStorageFileName = blobstoragefilename;
                uploadPayload.document.content = ConvertToBase64(filestream);
                return uploadPayload;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
               
            }

           // return uploadPayload;
        }

        public string ConvertToBase64(Stream instream)
        {
            string filecontent = string.Empty;

            try
            {
                if (instream != null)
                {
                    byte[] bytes = new byte[instream.Length];
                    using (MemoryStream ms = new MemoryStream())
                    {
                        //await e.File.OpenReadStream().CopyToAsync(ms);
                        instream.CopyToAsync(ms);
                        filecontent = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There is a problem with file content - {0}" + ex.Message);
            }

            return filecontent;
           
        }
    }
}
