using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace RACE2.DatabaseProvider
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

        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
               new Client
               {
                    ClientId = "company-employee",
                    ClientSecrets = new [] { new Secret("codemazesecret".Sha512()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId }
                }
            };

        public static List<TestUser> GetUsers() =>
            new List<TestUser>
            {
                            new TestUser
                            {
                                SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b4",
                                Username = "Mick",
                                Password = "MickPassword",
                                Claims = new List<Claim>
                                {
                                    new Claim("given_name", "Mick"),
                                    new Claim("family_name", "Mining")
                                }
                            },
                            new TestUser
                            {
                                SubjectId = "c95ddb8c-79ec-488a-a485-fe57a1462340",
                                Username = "Jane",
                                Password = "JanePassword",
                                Claims = new List<Claim>
                                {
                                    new Claim("given_name", "Jane"),
                                    new Claim("family_name", "Downing")
                                }
                            }
            };
    }
}
