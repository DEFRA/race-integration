using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.KeyVaultExtensions;
using Microsoft.IdentityModel.Tokens;
using RACE2.GovUK.OneloginAuth.Configuration;
using RACE2.GovUK.OneloginAuth.Services;


namespace RACE2.GovUK.OneloginAuth.AppStart
{
    internal static class ConfigureGovUkAuthenticationExtension
    {
        internal static void ConfigureGovUkAuthentication(this IServiceCollection services, IConfiguration configuration, string redirectUrl, string cookieDomain)
        {
            services
                .AddAuthentication(sharedOptions =>
                {
                    sharedOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                    sharedOptions.DefaultSignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddOpenIdConnect(options =>
                {
                    var govUkConfiguration = configuration.GetSection(nameof(GovUkOidcConfiguration));

                    options.SignInScheme=CookieAuthenticationDefaults.AuthenticationScheme;
                    options.ClientId = govUkConfiguration["ClientId"];
                    options.MetadataAddress = $"{govUkConfiguration["BaseUrl"]}/.well-known/openid-configuration";
                    options.ResponseType = "code";
                    options.AuthenticationMethod = OpenIdConnectRedirectBehavior.RedirectGet;
                    options.SignedOutRedirectUri = "/";
                    options.SignedOutCallbackPath = "/signed-out";
                    options.CallbackPath = "/sign-in";
                    options.RemoteSignOutPath= "/signed-out";
                    options.ResponseMode = string.Empty;
                    options.SaveTokens = true;
                    var scopes = "openid email phone".Split(' ');
                    options.Scope.Clear();
                    foreach (var scope in scopes)
                    {
                        options.Scope.Add(scope);
                    }
                    options.GetClaimsFromUserInfoEndpoint = true;

                    options.Events.OnRemoteFailure = c =>
                    {
                        if (c.Failure != null && c.Failure.Message.Contains("Correlation failed"))
                        {
                            c.Response.Redirect("/");
                            c.HandleResponse();
                        }

                        return Task.CompletedTask;
                    };
                    options.Events.OnUserInformationReceived += eventArgs =>
                    {
                        // We get the AccessToken from the ProtocolMessage.
                        // WARNING: This might change based on what type of Authentication Provider you are using
                        var accessToken = eventArgs.ProtocolMessage.AccessToken;
                        var idToken = eventArgs.ProtocolMessage.IdToken;
                        eventArgs.Principal.AddIdentity(new ClaimsIdentity(
                            new Claim[]
                            {
                                // Make note of the claim with the name "access_token"
                                // We will use it in an Authentication Service for look up.
                                new Claim("access_token", accessToken),
                                new Claim("id_token", idToken)
                            }
                        ));

                        // Here we take the accessToken and put all the claims into another
                        // Identity on the users Principal, giving us access to them when needed.
                        var jwtToken = new JwtSecurityToken(accessToken);
                        eventArgs.Principal.AddIdentity(new ClaimsIdentity(
                            jwtToken.Claims,
                            "jwt",
                            eventArgs.Options.TokenValidationParameters.NameClaimType,
                            eventArgs.Options.TokenValidationParameters.RoleClaimType
                        ));
                        return Task.CompletedTask;
                    };
                })
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = new PathString("/error/403");
                    
                    options.Cookie.Name = GovUkConstants.AuthCookieName;
                    if (!string.IsNullOrEmpty(cookieDomain))
                    {
                        options.Cookie.Domain = cookieDomain;
                    }
                    options.Cookie.IsEssential = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.CookieManager = new ChunkingCookieManager { ChunkSize = 3000 };
                    options.LogoutPath = "/logout";
                });
            services
                .AddOptions<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme)
                .Configure<IOidcService, IAzureIdentityService, ICustomClaims, GovUkOidcConfiguration, ITicketStore>(
                    (options, oidcService, azureIdentityService, customClaims, config, ticketStore) =>
                    {
                        var govUkConfiguration =config;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            AuthenticationType = "private_key_jwt",
                            IssuerSigningKey = new KeyVaultSecurityKey(govUkConfiguration.KeyVaultIdentifier,
                                azureIdentityService.AuthenticationCallback),
                            ValidateIssuerSigningKey = true,
                            ValidateIssuer = true,
                            ValidIssuer = govUkConfiguration.BaseUrl + @"/",
                            ValidateAudience = true,
                            SaveSigninToken = true,
                            NameClaimType = "name",
                            RoleClaimType = "role"
                        };
                        options.Events.OnAuthorizationCodeReceived = async (ctx) =>
                        {
                            var token = await oidcService.GetToken(ctx.TokenEndpointRequest);
                            if (token?.AccessToken!=null && token.IdToken !=null)
                            {
                                ctx.HandleCodeRedemption(token.AccessToken, token.IdToken);
                            }
                        };
                        options.Events.OnSignedOutCallbackRedirect = c =>
                        {
                            c.Response.Cookies.Delete(GovUkConstants.AuthCookieName);
                            c.Response.Redirect(redirectUrl);
                            c.HandleResponse();
                            return Task.CompletedTask;
                        };
                        options.Events.OnTokenValidated = async ctx => await oidcService.PopulateAccountClaims(ctx);                        
                    });
            services
                .AddOptions<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme)
                .Configure<ITicketStore, GovUkOidcConfiguration>((options, ticketStore, config) =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(config.LoginSlidingExpiryTimeOutInMinutes);
                    options.SlidingExpiration = true;
                    options.SessionStore = ticketStore;
                });
            
        }
    }
}