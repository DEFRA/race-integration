using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace RACE2.SecurityProvider
{
    public  static class Config
    {
        public static List<IdentityResource> IdentityResources
        {
            get
            {
                List<IdentityResource> idResources =
                    new List<IdentityResource>();
                            idResources.Add(new IdentityResources.OpenId());
                            idResources.Add(new IdentityResources.Profile());
                            idResources.Add(new IdentityResources.Email());
                            idResources.Add(new IdentityResources.Phone());
                            idResources.Add(new IdentityResources.Address());
                            idResources.Add(new IdentityResource("roles","User roles", new List<string> { "role" }));
                return idResources;
            }
        }

        public static List<ApiScope> ApiScopes
        {
            get
            {
                List<ApiScope> apiScopes =
                    new List<ApiScope>();
                            apiScopes.Add(new ApiScope("race2WebApi", "RACE2 Web API"));
                            return apiScopes;
                        }
        }

        public static List<ApiResource> ApiResources
        {
            get
            {
                ApiResource apiResource1 = new
                        ApiResource("race2WebApiResource", "RACE2 Web API")
                                {
                                    Scopes = { "race2WebApi" },
                                    UserClaims = { "role",
                                            "given_name",
                                            "family_name",
                                            "email",
                                            "phone",
                                            "address"
                                            }
                                };

                List<ApiResource> apiResources = new
                    List<ApiResource>();
                            apiResources.Add(apiResource1);

                return apiResources;
            }
        }

        public static List<Client> ClientsDev()        {
            
            List<string> redirectUris = new List<string>();
            redirectUris.Add("http://localhost:5001" + "/signin-oidc");
            List<string> postLogoutRedirectUris = new List<string>();
            postLogoutRedirectUris.Add("http://localhost:5001" + "/signout-callback-oidc");
            List<Client> clients = new List<Client>();
            Client blazorserverClient = new Client
            {
                ClientId = "blazorServer",
                ClientSecrets = { new Secret("blazorserver-secret".Sha512()) },
                ClientName = "Blazor Server",
                AllowedGrantTypes = GrantTypes.Hybrid,
                RequirePkce = false,
                RequireConsent = false,
                AllowPlainTextPkce = false,
                RedirectUris = { "http://localhost:5001" + "/signin-oidc" },
                FrontChannelLogoutUri = "http://localhost:5001" + "/signin-oidc",
                PostLogoutRedirectUris = { "http://localhost:5001" + "/signout-callback-oidc" },
                AllowOfflineAccess = true,
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Address,
                    "roles",
                    "race2WebApi"
                }
            };         

            clients.Add(blazorserverClient);

            return clients;
        }
        public static List<Client> ClientsProd()
        {            
            List<string> redirectUris = new List<string>();
            redirectUris.Add("https://race2frontendwebserver.politemeadow-dcdc1a32.westeurope.azurecontainerapps.io" + "/signin-oidc");
            List<string> postLogoutRedirectUris = new List<string>();
            postLogoutRedirectUris.Add("https://race2frontendwebserver.politemeadow-dcdc1a32.westeurope.azurecontainerapps.io" + "/signout-callback-oidc");
            List<Client> clients = new List<Client>();
            Client blazorserverClient = new Client
            {
                ClientId = "blazorServer",
                ClientSecrets = { new Secret("blazorserver-secret".Sha512()) },
                ClientName = "Blazor Server",
                AllowedGrantTypes = GrantTypes.Hybrid,
                RequirePkce = false,
                RequireConsent = false,
                AllowPlainTextPkce = false,
                RedirectUris = { "https://race2frontendwebserver.politemeadow-dcdc1a32.westeurope.azurecontainerapps.io" + "/signin-oidc" },
                FrontChannelLogoutUri = "https://race2frontendwebserver.politemeadow-dcdc1a32.westeurope.azurecontainerapps.io" + "/signin-oidc",
                PostLogoutRedirectUris = { "https://race2frontendwebserver.politemeadow-dcdc1a32.westeurope.azurecontainerapps.io" + "/signout-callback-oidc" },
                AllowOfflineAccess = true,
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Address,
                    "roles",
                    "race2WebApi"
                }
            };
            
            clients.Add(blazorserverClient);

            return clients;
        }

        public static List<TestUser> TestUsers =>
             new List<TestUser>
             {
                    new TestUser
                    {
                        SubjectId = "123",
                        Username = "kriss.sahoo@capgemini.com",
                        Password = "Saudamini123!",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Kris Sahoo"),
                            new Claim(JwtClaimTypes.GivenName, "Kris"),
                            new Claim(JwtClaimTypes.FamilyName, "Sahoo"),
                            //new Claim(JwtClaimTypes.WebSite, "https://gowthamcbe.com/"),
                        }
                    }
               };
    }
}
