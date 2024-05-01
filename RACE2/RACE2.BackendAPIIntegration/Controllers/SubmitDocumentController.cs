using Microsoft.AspNetCore.Mvc;
using RACE2.BackendAPIIntegration.Services;
using RACE2.Dto;
using RACE2.Services;
using static RACE2.Dto.DocumentDetails;

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
        public async Task<IntegrationResponseModel> SubmitStatement([FromForm]SubmitS12Statement submitS12Statement)
        {
            IntegrationResponseModel integrationResponseModel = new IntegrationResponseModel();
            if (submitS12Statement == null)
            {
                 integrationResponseModel.StatusCode = System.Net.HttpStatusCode.BadRequest;

                return integrationResponseModel;
            }

            try
            {
                integrationResponseModel =  await _uploadService.SubmitDocumentToBackend(submitS12Statement);

                return integrationResponseModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
