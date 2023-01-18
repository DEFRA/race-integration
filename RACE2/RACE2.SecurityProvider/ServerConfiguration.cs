using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace RACE2.SecurityProvider
{
    public static class ServerConfiguration
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

        public static List<Client> Clients
        {
            get
            {
                Client client1 = new Client
                {
                    ClientId = "client1",
                    ClientName = "Client 1",
                    ClientSecrets = new[] {
                        new Secret("client1_secret_code".Sha512()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = {
                        "openid",
                        "roles",
                        "race2WebApi"
                    }
                };

                Client client2 = new Client
                {
                    ClientId = "blazorWASM",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedCorsOrigins = { "http://localhost:5003" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "race2WebApi",
                        "roles"
                    },
                    RedirectUris = { "http://localhost:5003/authentication/login-callback" },
                    PostLogoutRedirectUris = { "http://localhost:5003/authentication/logout-callback" }
                };

                List<Client> clients = new List<Client>();
                clients.Add(client1);
                clients.Add(client2);

                return clients;
            }
        }        
    }
}
