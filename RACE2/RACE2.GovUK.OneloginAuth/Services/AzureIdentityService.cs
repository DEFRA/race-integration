using System;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace RACE2.GovUK.OneloginAuth.Services
{
    internal class AzureIdentityService : IAzureIdentityService
    {
        //public async Task<string> AuthenticationCallback(string authority, string resource, string scope)
        //{
        //    var chainedTokenCredential = new ChainedTokenCredential(
        //        new ManagedIdentityCredential(options:new TokenCredentialOptions
        //        {
        //            Retry = { NetworkTimeout = TimeSpan.FromMilliseconds(500),MaxRetries = 2, Delay = TimeSpan.FromMilliseconds(100)}
        //        }),
        //        new AzureCliCredential());

        //    var token = await chainedTokenCredential.GetTokenAsync(
        //        new TokenRequestContext(scopes: new[] {"https://vault.azure.net/.default"}));

        //    return token.Token;
        //}

        public async Task<string> AuthenticationCallback(string authority, string resource, string scope)
        {
            var azureAppConfigUrl = "https://pocracinfac1401.azconfig.io/";
            var azureTenantId = "770a2450-0227-4c62-90c7-4e38537f1102";
            var managedIdentityClientId = "ed22ed2e-445d-470f-989c-4f9d3d1f7a20";
            //var azureAppConfigUrl = "https://pocracinfac1402.azconfig.io/";
            //var azureTenantId = "6f504113-6b64-43f2-ade9-242e05780007";
            //var managedIdentityClientId = "80026cc9-216c-4002-8d7c-f4648af0f20f";
            var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { TenantId = azureTenantId, ManagedIdentityClientId = managedIdentityClientId, VisualStudioTenantId = azureTenantId });

            //var credential = new DefaultAzureCredential();
            var token = await credential.GetTokenAsync(
                new TokenRequestContext(scopes: new[] { "https://vault.azure.net/.default" }));

            return token.Token;
        }
    }
}