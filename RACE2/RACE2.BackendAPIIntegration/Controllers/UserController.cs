using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RACE2.BackendAPIIntegration.Authentication;
using RACE2.BackendAPIIntegration.Data;
using RACE2.Services;

namespace RACE2.BackendAPIIntegration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly Pocracinfdb1402Context _dbContext;
        private readonly IRACEIntegrationService _userService;
        private readonly ILogger<SubmitDocumentController> _logger;

        public UserController(Pocracinfdb1402Context dbContext,IRACEIntegrationService userService, ILogger<SubmitDocumentController> logger)
        {
            _dbContext = dbContext;
            _userService = userService;
            _logger = logger;
        }

        [ApiKeyAuthFilter]
        [HttpGet]
        public async Task<IEnumerable<AspNetUser>> Get()
        {
            return await _dbContext.AspNetUsers.ToListAsync();
        }



        [HttpPost]
        public async Task<IActionResult> Post(AspNetUser product)
        {
            _dbContext.Add(product);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(AspNetUser userData)
        {
            if (userData == null || userData.Id == 0)
                return BadRequest();

            var product = await _dbContext.AspNetUsers.FindAsync(userData.Id);
            if (product == null)
                return NotFound();
            //product.Name = productData.Name;
            //product.Description = productData.Description;
            //product.Price = productData.Price;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
