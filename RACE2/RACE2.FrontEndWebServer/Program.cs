using Azure.Identity;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.IdentityModel.Logging;
using RACE2.DataAccess.Repository;
using RACE2.DataModel;
using RACE2.Services;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using RACE2.Notification;
using RACE2.FrontEndWebServer.ExceptionGlobalErrorHandling;
using RACE2.FrontEndWebServer.Components;
using RACE2.GovUK.OneloginAuth.Configuration;
using RACE2.FrontEndWebServer.AppStart;
using System.Configuration;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.IdentityModel.Tokens;
using RACE2.GovUK.OneloginAuth.Services;
using Microsoft.AspNetCore.Authorization;
using RACE2.FrontEndWebServer;
using RACE2.GovUK.OneloginAuth.Authentication;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddAzureAppConfiguration(options =>
    {
        //var connectionString = builder.Configuration["AZURE_APPCONFIGURATION_CONNECTIONSTRING"];
        var azureAppConfigUrl = builder.Configuration["AzureAppConfigURL"];
        var azureTenantId = builder.Configuration["AZURE_TENANT_ID"];
        var managedIdenityClientId = builder.Configuration["ManagedIdenityClientId"];
        var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { TenantId = azureTenantId, ManagedIdentityClientId = managedIdenityClientId, VisualStudioTenantId = azureTenantId });

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

    //builder.Services.Configure<GovUkOidcConfiguration>(builder.Configuration.GetSection(nameof(GovUkOidcConfiguration)));
    builder.Services.AddServiceRegistration(builder.Configuration);

    var blazorClientURL = builder.Configuration["RACE2FrontEndURL"];
    var RACE2WebApiURL = builder.Configuration["RACE2WebApiURL"];
    var RACE2IDPURL = builder.Configuration["RACE2SecurityProviderURL"];
    var clientSecret = builder.Configuration["ClientSecret"];
    var appinsightsConnString = builder.Configuration["AppInsightsConnectionString"];
    var sqlConnectionString = builder.Configuration["SqlConnectionString"];
    //IConfiguration _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../appsettings.json").Build();

    //Serilog Use
    var tableName = "Logs";
    var columnOptions = new ColumnOptions();
    builder.Host.UseSerilog((ctx, lc) => lc
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.MSSqlServer(sqlConnectionString, tableName, columnOptions: columnOptions)
        .WriteTo.ApplicationInsights(new TelemetryConfiguration { ConnectionString = appinsightsConnString }, TelemetryConverter.Traces));

    builder.Services.AddApplicationInsightsTelemetry(options =>
    {
        options.ConnectionString = appinsightsConnString;
    });

    // Add services to the container.
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();

    builder.Services.AddRazorPages().WithRazorPagesRoot("/Pages");
  
    builder.Services.Configure<CookiePolicyOptions>(options =>
    {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
    });

    bool requireHttpsMetadata = builder.Environment.IsProduction();
    
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IReservoirService, ReservoirService>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IReservoirRepository, ReservoirRepository>();
    builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();
    builder.Services.AddScoped<IOpenXMLUtilitiesService, OpenXMLUtilitiesService>();
    builder.Services.AddScoped<CustomErrorBoundary>();
    builder.Services.AddScoped<INotification, RaceNotification>();

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
    builder.Services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
    builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, BlazorAuthorizationMiddlewareResultHandler>();
    builder.Services.AddSingleton<BaseUrlProvider>();
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(
            PolicyNames.IsAuthenticated, policy =>
            {
                policy.Requirements.Add(new AccountActiveRequirement());
                policy.RequireAuthenticatedUser();
            });
        options.AddPolicy(
            PolicyNames.IsActiveAccount, policy =>
            {
                policy.Requirements.Add(new AccountActiveRequirement());
                policy.RequireAuthenticatedUser();
            });
    });
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }
    //app.UseSerilogRequestLogging(configure =>
    //{
    //    configure.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000}ms";
    //}); // We want to log all HTTP requests
    app.UseSerilogRequestLogging();
    //app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseAntiforgery();
    //app.SetupEndpoints();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapRazorPages();
    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode();

    app.Run();
}
catch (Exception ex)
{
    Serilog.Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Serilog.Log.CloseAndFlush();
}
