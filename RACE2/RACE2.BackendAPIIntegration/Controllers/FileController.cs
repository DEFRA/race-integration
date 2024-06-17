using Microsoft.AspNetCore.Mvc;
using RACE2.BackendAPIIntegration.Services;
using RACE2.Dto;

namespace RACE2.BackendAPIIntegration.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FileController : Controller
    {
        private readonly IFileService _uploadService;
      
        public FileController(IFileService uploadService)
        {
            _uploadService = uploadService;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        /// <summary>
        /// Single File Upload
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("PostSingleFile")]
        public async Task<ActionResult> PostSingleFile([FromForm] FileUploadModel fileDetails)
        {
            if (fileDetails == null)
            {
                return BadRequest();
            }

            try
            {
                await _uploadService.PostFileAsync(fileDetails.FileDetails, fileDetails.FileType);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class FileUploadModel
    {
        public IFormFile FileDetails { get; set; }
        public FileType FileType { get; set; }
    }
}
