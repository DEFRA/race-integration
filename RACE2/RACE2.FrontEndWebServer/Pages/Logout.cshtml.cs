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
    {
        // just to remove compiler warning
        await Task.CompletedTask;

        var idToken = await HttpContext.GetTokenAsync("id_token");
        var accessToken = await HttpContext.GetTokenAsync("access_token");

        foreach (var cookie in HttpContext.Request.Cookies.Keys)
        {
            HttpContext.Response.Cookies.Delete(cookie);
        }
        Response.Redirect("https://oidc.integration.account.gov.uk/logout");
        //SignOut(OpenIdConnectDefaults.AuthenticationScheme, CookieAuthenticationDefaults.AuthenticationScheme);
        //Response.Redirect("/");
    }
  }
}
