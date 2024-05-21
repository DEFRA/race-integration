using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
namespace RACE2.FrontEndWebServer;

public static class AuthenticationExtensions
{
    public static IEndpointRouteBuilder SetupEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/Account/Login", async (HttpContext httpContext, string returnUrl = "/") =>
        {
            await httpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = !string.IsNullOrEmpty(returnUrl) ? returnUrl : "/"
                });
        });

        app.MapPost("/Account/Logout", async (HttpContext httpContext) =>
        {
            var authenticationProperties = new AuthenticationProperties
            {
                // needs to be added to the Auth0 Allowed Logout URLs
                // RedirectUri = "/SignedOut" 
                RedirectUri = "/"
            };
            if (httpContext.Request.Cookies.Count > 0)
            {
                var siteCookies = httpContext
                    .Request
                    .Cookies
                    .Where(c => c.Key.Contains(".AuthCookie")
                        || c.Key.Contains(".AspNetCore.")
                        || c.Key.Contains("Microsoft.Authentication"));

                foreach (var cookie in siteCookies)
                {
                    httpContext.Response.Cookies.Delete(cookie.Key);
                }
            }

            await httpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme,
                authenticationProperties);

            await httpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

        }).RequireAuthorization();

        return app;
    }
}