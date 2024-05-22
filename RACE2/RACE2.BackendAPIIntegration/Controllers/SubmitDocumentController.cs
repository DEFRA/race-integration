using Microsoft.AspNetCore.Mvc;
using RACE2.BackendAPIIntegration.Services;
using RACE2.Dto;
using RACE2.Services;


namespace RACE2.BackendAPIIntegration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubmitDocumentController : Controller
    {
        private readonly IRACEIntegrationService _uploadService;
        private readonly ILogger<SubmitDocumentController> _logger;

        public SubmitDocumentController(IRACEIntegrationService uploadService,ILogger<SubmitDocumentController> logger)
        { 
            _uploadService = uploadService;
            _logger = logger;
        }
    

        [HttpPost("SubmitStatement")]
        public async Task<IActionResult> SubmitStatement([FromBody]AnnualSubmissionDocumentDetails submitS12Statement)
        {
            IntegrationResponseModel integrationResponseModel = new IntegrationResponseModel();
            if (submitS12Statement == null)
            {
                 integrationResponseModel.StatusCode = System.Net.HttpStatusCode.BadRequest;

                return BadRequest();
            }

            try
            {
                integrationResponseModel =  await _uploadService.SubmitDocumentToBackend(submitS12Statement);
                _logger.LogInformation("API REsults" + integrationResponseModel.StatusCode);
                _logger.LogInformation("Reason" + integrationResponseModel.ResponseData);
                if((integrationResponseModel.StatusCode == System.Net.HttpStatusCode.OK) && (integrationResponseModel.ResponseData == "200 OK Submission Accepted")) 
                    {
                    return Ok();
                }

                else
                {
                    return BadRequest();
                }

               
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
