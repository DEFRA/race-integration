using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace RACE2.FrontEndWebServer.Pages
{
  public class LogoutModel : PageModel
  {
    public async Task OnGetAsync()
    //public async Task<ActionResult> OnGetAsync()
        {
        // just to remove compiler warning
        //await Task.CompletedTask;
        var idToken = await HttpContext.GetTokenAsync("id_token");
        var accessToken = await HttpContext.GetTokenAsync("access_token");

        var authenticationProperties = new AuthenticationProperties();
        authenticationProperties.Parameters.Clear();
        authenticationProperties.Parameters.Add("id_token", idToken);
        authenticationProperties.Parameters.Add("access_token", accessToken);

        //return SignOut(
        //    authenticationProperties,
        //    new[] {
        //CookieAuthenticationDefaults.AuthenticationScheme,
        //OpenIdConnectDefaults.AuthenticationScheme
        //    });

        //HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
        //return SignOut(OpenIdConnectDefaults.AuthenticationScheme, CookieAuthenticationDefaults.AuthenticationScheme);
        Response.Redirect("https://oidc.integration.account.gov.uk/logout");

        }
  }
}
