using System;
using Azure.Identity;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using RACE2.DataAccess.Repository;
using RACE2.Services;

[assembly: FunctionsStartup(typeof(FunctionApp.Startup))]

namespace FunctionApp
{
   public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
           // string cs = Environment.GetEnvironmentVariable("ConnectionString");
            builder.ConfigurationBuilder.AddAzureAppConfiguration(options =>
            {
                //var connectionString = builder.Configuration["AZURE_APPCONFIGURATION_CONNECTIONSTRING"];
                var azureAppConfigUrl = Environment.GetEnvironmentVariable("AzureAppConfigURL");
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
            });


        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
                     

            //builder.Services.AddSingleton<IUserService, UserService>();
            //builder.Services.AddSingleton<IReservoirService, ReservoirService>();
            //builder.Services.AddSingleton<IUserRepository, UserRepository>();
            //builder.Services.AddSingleton<IReservoirRepository, ReservoirRepository>();
        }
    }
}