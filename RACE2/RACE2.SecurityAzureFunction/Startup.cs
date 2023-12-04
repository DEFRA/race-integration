using Azure.Identity;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: FunctionsStartup(typeof(RACE2.SecurityAzureFunction.Startup))]



namespace RACE2.SecurityAzureFunction
{
    public class Startup : FunctionsStartup
    {

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            // string cs = Environment.GetEnvironmentVariable("ConnectionString");
                        
            builder.ConfigurationBuilder.AddAzureAppConfiguration(options =>
            {
               // var context = builder.GetContext();
              //  var azureAppConfigUrl = context.Configuration["AzureAppConfigUrl"];
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

            var context = builder.GetContext();
            var configuration = context.Configuration;

           // var appinsightsConnString = configuration["AppInsightsConnectionString"];
            //var sqlConnectionString = configuration["SqlConnectionString"];
            


            //builder.Services.AddScoped<IUserRepository, UserRepository>();
            //builder.Services.AddScoped<IUserService,UserService>();
            // builder.Services.AddHttpClient();

            //builder.Services.AddSingleton<IMyService>((s) => {
            //    return new MyService();
            //});

            //builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
        }
    }
}
