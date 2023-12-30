using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.AppConfiguration.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Options;
using RACE2.Services;
using RACE2.DataAccess.Repository;

var host = new HostBuilder()
     .ConfigureAppConfiguration(builder =>
     {
         builder.AddAzureAppConfiguration(options =>
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
     })
    .ConfigureServices(services =>
    {
        // Make Azure App Configuration services available through dependency injection.
        services.AddAzureAppConfiguration();
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IReservoirRepository, ReservoirRepository>();
        services.AddTransient<IReservoirService, ReservoirService>();
    })
    .ConfigureFunctionsWorkerDefaults(app =>
    {
        // Use Azure App Configuration middleware for data refresh.
        app.UseAzureAppConfiguration();
    })
    .Build();
    
host.Run();
