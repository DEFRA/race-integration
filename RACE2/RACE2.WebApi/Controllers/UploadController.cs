using Microsoft.AspNetCore.Mvc;

namespace RACE2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            // Check if the file is there
            if (file == null)
                return BadRequest("File is required");

            // Get the file name
            var fileName = file.FileName;

            // Get the extension
            var extension = System.IO.Path.GetExtension(fileName);

            // Validate the extension based on your business needs

            // Generate a new file to avoid duplicates = (FileName withoutExtension - GUId.extension)
            var newFileName = $"{System.IO.Path.GetFileNameWithoutExtension(fileName)}-{Guid.NewGuid().ToString()}{extension}";

            // Create the full path of the file including the directory (For this demo we will save the file inside a folder called Data within wwwroot)
            var directoryPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Data");
            var fullPath = System.IO.Path.Combine(directoryPath, newFileName);

            // Make sure the directory is there by creating it if it's not exist
            Directory.CreateDirectory(directoryPath);

            // Create a new file stream where you want to put your file and copy the content from the current file stream to the new one
            using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                // Copy the content to the new stream
                await file.CopyToAsync(fileStream);
            }

            // You are done return the new URL which is (your application url/data/newfilename)
            return Ok($"https://localhost:5003/Data/{newFileName}");
        }
    }
}
