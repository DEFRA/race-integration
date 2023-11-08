using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using RACE2.DataAccess;
using RACE2.DatabaseProvider.Data;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.Notification;
using RACE2.SecurityProvider;
using RACE2.SecurityProvider.UtilityClasses;
using RACE2.SecurityProvider.UtilityClasses.CompanyEmployees.OAuth.Extensions;
using RACE2.Services;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Configuration;
using System.Data.SqlClient;

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
    var blazorClientURL = builder.Configuration["RACE2FrontEndURL"];
    var webapiURL = builder.Configuration["RACE2WebApiURL"];
    var securityProviderURL = builder.Configuration["RACE2SecurityProviderURL"];
    var sqlConnectionString = builder.Configuration["SqlConnectionString"];
    var appinsightsConnString = builder.Configuration["AppInsightsConnectionString"];

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

    // Add Azure App Configuration and feature management services to the container.
    builder.Services.AddAzureAppConfiguration()
                    .AddFeatureManagement();
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(sqlConnectionString));

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddDefaultIdentity<UserDetail>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);//.FromDays(365);//.FromMinutes(10);//default 5
            options.Lockout.MaxFailedAccessAttempts = 5;//default 5
        })
        .AddRoles<Role>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

    builder.Services.AddRazorPages();

    var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
    builder.Services.AddIdentityServer()
        .AddConfigurationStore(options =>
        {
            options.ConfigureDbContext = b => b.UseSqlServer(sqlConnectionString,
                sql => sql.MigrationsAssembly(migrationsAssembly));
        })
        .AddOperationalStore(options =>
        {
            options.ConfigureDbContext = b => b.UseSqlServer(sqlConnectionString,
                sql => sql.MigrationsAssembly(migrationsAssembly));
        })
        .AddDeveloperSigningCredential()
        .AddAspNetIdentity<UserDetail>();

    builder.Services.Configure<IdentityOptions>(options =>
    {
        // Default Password settings.
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredUniqueChars = 1;
    });

    builder.Services.AddScoped<IRandomPasswordGeneration, RandomPasswordGeneration>();
    builder.Services.AddScoped<INotification, RaceNotification>();
    builder.Services.AddSingleton<ICorsPolicyService>((container) =>
    {
        var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
        return new DefaultCorsPolicyService(logger)
        {
            AllowedOrigins = { blazorClientURL, webapiURL }
        };
    });
    builder.Services.AddDataProtection();

    var app = builder.Build();
    app.Use(async (ctx, next) =>
    {
        ctx.SetIdentityServerOrigin(securityProviderURL);
        await next();
    });

    // Configure the HTTP request pipeline.

    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Identity/Account/Error");
        app.UseHsts();
    }

    //app.UseSerilogRequestLogging(configure =>
    //{
    //    configure.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000}ms";
    //}); // We want to log all HTTP requests
    app.UseSerilogRequestLogging();
    // Use Azure App Configuration middleware for dynamic configuration refresh.
    app.UseAzureAppConfiguration();

    app.UseHttpsRedirection();

    HostingExtensions.InitializeDatabase(app);//seed initial data

    app.UseCookiePolicy(new CookiePolicyOptions
    {
        MinimumSameSitePolicy = SameSiteMode.Lax
    });
    app.UseStaticFiles();

    app.UseRouting();

    app.UseIdentityServer();
    //app.UseAuthentication();
    app.UseAuthorization();

    app.MapRazorPages();

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
