using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace RACE2.SecurityProvider
{
    public  static class ServerConfiguration
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

        public static List<Client> Clients(string blazorClientURL)
        {
            //List<string> allowedCorsOrigins = new List<string>();
            //allowedCorsOrigins.Add(blazorClientURL);
            List<string> redirectUris = new List<string>();
            redirectUris.Add(blazorClientURL + "/signin-oidc");
            List<string> postLogoutRedirectUris = new List<string>();
            postLogoutRedirectUris.Add(blazorClientURL + "/signout-callback-oidc");
            List<Client> clients = new List<Client>();
            Client blazorserverClient = new Client
            {
                ClientId = "blazorServer",
                ClientSecrets = { new Secret("blazorserver-secret".Sha512()) },
                ClientName = "Blazor Server",
                AllowedGrantTypes = GrantTypes.Hybrid,
                RequirePkce = false,
                //AllowPlainTextPkce = false,
                RequireConsent = false,
                //AllowedCorsOrigins = allowedCorsOrigins,
                RedirectUris = redirectUris,
                //FrontChannelLogoutUri =  blazorClientURL + "/signin-oidc",
                PostLogoutRedirectUris = postLogoutRedirectUris,
                //AllowOfflineAccess = true,
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Address,
                    "roles",
                    "race2WebApi"
                }                
            };

            //Client blazorwasmClient = new Client
            //{
            //    ClientId = "blazorWASM",
            //    AllowedGrantTypes = GrantTypes.Code,
            //    RequirePkce = true,
            //    RequireClientSecret = false,
            //    AllowedCorsOrigins = allowedCorsOrigins,
            //    AllowedScopes =
            //    {
            //        IdentityServerConstants.StandardScopes.OpenId,
            //        IdentityServerConstants.StandardScopes.Profile,
            //        "race2WebApi",
            //        "roles"
            //    },
            //    RedirectUris = redirectUris,
            //    PostLogoutRedirectUris = postLogoutRedirectUris
            //};

            //Client webapiClient = 
            //    new Client
            //    {
            //        ClientId = "webapi",
            //        ClientSecrets = new List<Secret> { new("secret".Sha512()) },
            //        ClientName = "Banana Cake Pop",
            //        AllowedGrantTypes = GrantTypes.Code,
            //        AllowedScopes = new List<string>
            //        {
            //            "openid",
            //            "profile",
            //            "email",
            //            "role",
            //            "race2WebApi"
            //        },
            //        AllowedCorsOrigins = allowedCorsOrigins,
            //        RedirectUris = redirectUris
            //    };

            clients.Add(blazorserverClient);
            //clients.Add(blazorwasmClient);
            //clients.Add(webapiClient);
            return clients;
        }        
    }
}
