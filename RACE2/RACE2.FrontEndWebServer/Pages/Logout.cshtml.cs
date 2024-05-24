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
        var state = await HttpContext.GetTokenAsync("state");
        var baseUrl = "https://"+Request.Host;


            string post_logout_redirect_uri = baseUrl;
            string LogoutAPIurl = "https://oidc.integration.account.gov.uk/logout?id_token_hint={0}&post_logout_redirect_uri={1}";  //&state=af0ifjsldkj

            string requestUri = string.Format(LogoutAPIurl, idToken, post_logout_redirect_uri);


            Serilog.Log.Logger.ForContext("User", requestUri).ForContext("Application", "FrontEndWebServer").ForContext("Method", "Logout").Information(requestUri);
            // requestUri += "&Authorization=Bearer "+accessToken;

            foreach (var cookie in HttpContext.Request.Cookies.Keys)
            {
                HttpContext.Response.Cookies.Delete(cookie);
            }

            //var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            //request.Headers.Add("Authorization", "Bearer " + accessToken);

            //using (var client = new HttpClient())
            //{
            //   // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //    // client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            //    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            //    using (HttpResponseMessage response = await client.GetAsync(requestUri))
            //    {
            //        var responseContent = response.Content.ReadAsStringAsync().Result;
            //        response.EnsureSuccessStatusCode();
            //    }
            //}


            //working
            //  Response.Redirect("https://oidc.integration.account.gov.uk/logout");
            Response.Redirect(requestUri);
            Serilog.Log.Logger.ForContext("User", requestUri).ForContext("Application", "FrontEndWebServer").ForContext("Method", "Logout").Information("User Logged out successfully");

            // try this otherwise
            //string logoutRedirectUri = "https://oidc.integration.account.gov.uk?id_token_hint=" + idToken + "&post_logout_redirect_uri=" + baseUrl + "&state=sadk8d4--lda%d";
            //Response.Redirect(logoutRedirectUri);

            //try this otherwise
            //SignOut(OpenIdConnectDefaults.AuthenticationScheme, CookieAuthenticationDefaults.AuthenticationScheme);
            //Response.Redirect("/");
        }
  }
}
