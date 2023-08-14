using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RACE2.FrontEndWeb.Pages.Shared
{
    [Authorize]
    public class LogoutModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;

        public LogoutModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult OnGet()
        {
            SignOut("cookie", "oidc");
            var host = _configuration["RACE2SecurityProviderURL"];// "https://localhost:5010";
            var cookieName = "raceappcookie";
            var url = host + "/identity/account/logout";
            Response.Cookies.Delete(cookieName);
            return Redirect(url);
        }
    }
}
