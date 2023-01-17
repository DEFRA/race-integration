using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace RACE2.IdentityProvider
{
    public class ServerConfiguration
    {
        public static List<IdentityResource> IdentityResources
        {
            get
            {
                List<IdentityResource> idResources =  new List<IdentityResource>();
                idResources.Add(new IdentityResources.OpenId());
                idResources.Add(new IdentityResources.Profile());
                idResources.Add(new IdentityResources.Email());
                idResources.Add(new IdentityResources.Phone());
                idResources.Add(new IdentityResources.Address());
                idResources.Add(new IdentityResource("roles", "User roles", new List<string> { "role" }));
                return idResources;
            }
        }

        public static List<ApiScope> ApiScopes
        {
            get
            {
                List<ApiScope> apiScopes = new List<ApiScope>();
                apiScopes.Add(new ApiScope("race2WebApi", "Race2 Web API"));
                return apiScopes;
            }
        }

        public static List<ApiResource> ApiResources
        {
            get
            {
                ApiResource apiResource1 = new ApiResource("race2WebApiResource","Race2 Web API")             
                {
                    Scopes = { "race2WebApi" },
                    UserClaims = {  "role",
                                    "given_name",
                                    "family_name",
                                    "email",
                                    "phone",
                                    "address"
                                    }
                };

                List<ApiResource> apiResources = new List<ApiResource>();
                apiResources.Add(apiResource1);

                return apiResources;
            }
        }

        public static List<Client> Clients
        {
            get
            {
                Client client = new Client
                {
                    ClientId = "blazorWASM",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedCorsOrigins = { "https://localhost:5003" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "race2WebApi",
                        "roles"
                    },
                    RedirectUris = { "https://localhost:5003/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:5003/authentication/logout-callback" }
                };

                List<Client> clients = new List<Client>();
                clients.Add(client);

                return clients;
            }
        }

        public static List<TestUser> TestUsers
        {
            get
            {
                TestUser usr1 = new TestUser()
                {
                    SubjectId = "2f47f8f0-bea1-4f0e-ade1-88533a0eaf57",
                    Username = "krissahoo",
                    Password = "kris123",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Kris"),
                        new Claim("family_name", "Sahoo"),
                        new Claim("address", "UK"),
                        new Claim("email","kcsahoo@gmail.com"),
                        new Claim("phone", "07427623140"),
                        new Claim("role", "engineer"),
                        new Claim(JwtClaimTypes.Name,"Kris Sahoo")
                    }
                };

                TestUser usr2 = new TestUser()
                {
                    SubjectId = "5747df40-1bff-49ee-aadf-905bacb39a3a",
                    Username = "mahalaxmi",
                    Password = "maha123",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Mahalaxmi"),
                        new Claim("family_name", "lastName2"),
                        new Claim("address", "UK"),
                        new Claim("email","user2@localhost"),
                        new Claim("phone", "456"),
                        new Claim("role", "underwriter"),
                        new Claim(JwtClaimTypes.Name,"Mahalaxmi")
                    }
                };

                List<TestUser> testUsers = new List<TestUser>();
                testUsers.Add(usr1);
                testUsers.Add(usr2);

                return testUsers;
            }
        }
    }
}
