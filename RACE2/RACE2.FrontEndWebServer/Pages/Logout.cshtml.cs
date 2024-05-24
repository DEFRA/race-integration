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
        var baseUrl = "https://"+Request.Host;

        foreach (var cookie in HttpContext.Request.Cookies.Keys)
        {
            HttpContext.Response.Cookies.Delete(cookie);
        }

        //working
        Response.Redirect("https://oidc.integration.account.gov.uk/logout");

        // try this otherwise
        //string logoutRedirectUri = "https://oidc.integration.account.gov.uk?id_token_hint=" + idToken + "&post_logout_redirect_uri=" + baseUrl + "&state=sadk8d4--lda%d";
        //Response.Redirect(logoutRedirectUri);

        //try this otherwise
        //SignOut(OpenIdConnectDefaults.AuthenticationScheme, CookieAuthenticationDefaults.AuthenticationScheme);
        //Response.Redirect("/");
    }
  }
}
