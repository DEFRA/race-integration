using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RACE2.DataModel;
using System;
using System.Text.Json.Serialization;
using System.Text;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using Microsoft.Net.Http.Headers;
using IdentityModel.Client;
using System.Net.Http.Headers;

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
        //var state = await HttpContext.GetTokenAsync("state");
        string post_logout_redirect_uri = "https://race2frontendwebserver.proudbeach-8eb578a1.uksouth.azurecontainerapps.io";// "https://" + Request.Host;
        string LogoutAPIurl = "https://oidc.integration.account.gov.uk/logout?id_token_hint={0}&post_logout_redirect_uri={1}";  //&state=af0ifjsldkj

        string requestUri = string.Format(LogoutAPIurl, idToken, post_logout_redirect_uri);

        foreach (var cookie in HttpContext.Request.Cookies.Keys)
        {
            HttpContext.Response.Cookies.Delete(cookie);
        }       

        //working
        //Response.Redirect("https://oidc.integration.account.gov.uk/logout");
        Response.Redirect(requestUri);

        //try this otherwise
        //SignOut(OpenIdConnectDefaults.AuthenticationScheme, CookieAuthenticationDefaults.AuthenticationScheme);
        //Response.Redirect("/");
    }
  }
}
