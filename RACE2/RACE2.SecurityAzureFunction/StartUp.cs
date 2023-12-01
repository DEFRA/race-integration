using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using RACE2.SecurityAzureFunction;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Azure.Identity;

[assembly: WebJobsStartup(typeof(StartUp))]

public class StartUp : IWebJobsStartup
{
    public void Configure(IWebJobsBuilder builder)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddAzureAppConfiguration(options =>
            {
                //var connectionString = builder.Configuration["AzureAppConfigURL"];
                var azureAppConfigUrl = "https://pocracinfac1401.azconfig.io/"; // builder.Configuration["AzureAppConfigURL"];
                var credential = new DefaultAzureCredential();

                //options.Connect(connectionString)      
                options.Connect(new Uri(azureAppConfigUrl), credential)
                .ConfigureKeyVault(options =>
                {
                    options.SetCredential(credential);
                })
                .ConfigureRefresh(refreshOptions =>
                        refreshOptions.Register("refreshAll", refreshAll: true))
                .Select(KeyFilter.Any, LabelFilter.Null)
                // Override with any configuration values specific to current hosting env
                .Select(KeyFilter.Any, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                .UseFeatureFlags();
                })
            .Build();

        var blazorClientURL = config["RACE2FrontEndURL"];
        var webapiURL = config["RACE2WebApiURL"];
        var securityProviderURL = config["RACE2SecurityProviderURL"];
        var sqlConnectionString = config["SqlConnectionString"];
        var appinsightsConnString = config["AppInsightsConnectionString"];
    }
}