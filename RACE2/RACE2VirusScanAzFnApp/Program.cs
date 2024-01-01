using Azure.Identity;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RACE2.DataAccess.Repository;
using RACE2.Services;
using Microsoft.Azure.AppConfiguration.Functions.Worker;

var host = new HostBuilder()
    //.ConfigureAppConfiguration(builder =>
    //{
    //    builder.AddAzureAppConfiguration(options =>
    //    {
    //        var azureAppConfigUrl = Environment.GetEnvironmentVariable("AzureAppConfigURL");
    //        var credential = new DefaultAzureCredential();
    //        options.Connect(new Uri(azureAppConfigUrl), credential)
    //        .ConfigureKeyVault(kv =>
    //        {
    //            kv.SetCredential(credential);
    //        })
    //        .ConfigureRefresh(refreshOptions =>
    //                refreshOptions.Register("refreshAll", refreshAll: true))
    //        .Select(KeyFilter.Any, LabelFilter.Null)
    //        // Override with any configuration values specific to current hosting env
    //        .Select(KeyFilter.Any, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
    //    });
    //})
    .ConfigureServices(services =>
    {
        // Make Azure App Configuration services available through dependency injection.
        //services.AddAzureAppConfiguration();
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IReservoirRepository, ReservoirRepository>();
        services.AddTransient<IReservoirService, ReservoirService>();
    })
    .ConfigureFunctionsWorkerDefaults()
    //.ConfigureFunctionsWorkerDefaults(app =>
    //{
    //    // Use Azure App Configuration middleware for data refresh.
    //    app.UseAzureAppConfiguration();
    //})
    .Build();
    
host.Run();
