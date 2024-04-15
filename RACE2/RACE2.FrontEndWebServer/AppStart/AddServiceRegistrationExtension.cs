using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using RACE2.FrontEndWebServer.AppStart;
using RACE2.GovUK.OneloginAuth.AppStart;
using RACE2.GovUK.OneloginAuth.Authentication;

namespace RACE2.FrontEndWebServer.AppStart;

public static class AddServiceRegistrationExtension
{
    public static void AddServiceRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
        services.AddAndConfigureGovUkAuthentication(configuration, typeof(CustomClaims), "", "");
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                PolicyNames.IsAuthenticated, policy =>
                {
                    policy.Requirements.Add(new AccountActiveRequirement());
                    policy.RequireAuthenticatedUser();
                });
            options.AddPolicy(
                PolicyNames.IsActiveAccount, policy =>
                {
                    policy.Requirements.Add(new AccountActiveRequirement());
                    policy.RequireAuthenticatedUser();
                });
        });
    }
}