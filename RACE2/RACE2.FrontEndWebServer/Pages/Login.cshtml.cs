using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace RACE2.FrontEndWebServer.Pages
{
    public class LoginModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync(string redirectUri)
        {
            // just to remove compiler warning
            await Task.CompletedTask;

            if (string.IsNullOrWhiteSpace(redirectUri))
            {
                redirectUri = Url.Content("~/annual-statements");
            }

            // If user is already logged in, we can redirect directly...
            if (HttpContext.User.Identity!.IsAuthenticated)
            {
                Response.Redirect(redirectUri);
            }

            return Challenge(new AuthenticationProperties
            {
                RedirectUri = redirectUri
            },
            OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
