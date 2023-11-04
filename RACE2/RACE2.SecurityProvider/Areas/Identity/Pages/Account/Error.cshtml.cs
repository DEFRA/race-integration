using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RACE2.SecurityProvider.Areas.Identity.Pages
{
    public class ErrorModel : PageModel
    {
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// 
        private readonly IConfiguration _config;
        public string WebAppUrl { get; set; }

        public ErrorModel(IConfiguration config)
        {
            _config = config;
        }
        public void OnGet()
        {
            WebAppUrl = _config["RACE2FrontEndURL"];
            string returnUrl = _config["RACE2FrontEndURL"] + "/login";
            Redirect(returnUrl);
        }
    }
}
