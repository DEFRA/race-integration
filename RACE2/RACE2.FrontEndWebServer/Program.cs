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
        var managedIdentityClientId = builder.Configuration["ManagedIdentityClientId"];
        var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { TenantId = azureTenantId, ManagedIdentityClientId = managedIdentityClientId, VisualStudioTenantId = azureTenantId });

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
    //.AddHubOptions(options =>
    //    {
    //        options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);//.FromSeconds(30); 
    //        options.EnableDetailedErrors = true;
    //        options.HandshakeTimeout = TimeSpan.FromSeconds(15); //FromSeconds(15); 
    //        options.KeepAliveInterval = TimeSpan.FromSeconds(15);//.FromSeconds(15);  
    //        options.MaximumParallelInvocationsPerClient = 1;
    //        options.MaximumReceiveMessageSize = 128 * 1024; //32*1024;
    //        options.StreamBufferCapacity = 10;
    //    });
    builder.Services.AddRazorPages();

    builder.Services.Configure<CookiePolicyOptions>(options =>
    {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
    });
    builder.Services.AddHttpContextAccessor();

    bool requireHttpsMetadata = builder.Environment.IsProduction();
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    //.AddCookie(options =>
    //{
    //    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    //    options.Cookie.MaxAge = options.ExpireTimeSpan; // optional
    //    options.SlidingExpiration = true;
    //    options.LoginPath = "/login";
    //    options.LogoutPath = "/logout";
    //})
    .AddOpenIdConnect(
        OpenIdConnectDefaults.AuthenticationScheme,
        options =>
        {
            //options.Events.OnTicketReceived = async (Context) =>
            //{
            //    Context.Properties.ExpiresUtc = DateTime.UtcNow.AddMinutes(20);
            //};
            //options.Events.OnRedirectToIdentityProvider = context =>
            //{
            //    context.ProtocolMessage.Prompt = "login";
            //    return Task.CompletedTask;
            //};
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.SignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
            options.Authority = RACE2IDPURL;
            options.ClientId = "blazorServer";
            options.ClientSecret = clientSecret;
            // When set to code, the middleware will use PKCE protection
            options.ResponseType = "code id_token";
            // Save the tokens we receive from the IDP
            options.SaveTokens = true; // default false
            // It's recommended to always get claims from the UserInfoEndpoint during the flow.
            options.GetClaimsFromUserInfoEndpoint = true;
            options.Scope.Add("race2WebApi");
            options.RequireHttpsMetadata = requireHttpsMetadata;
            options.Events = new OpenIdConnectEvents
            {
                OnAccessDenied = context =>
                {
                    context.HandleResponse();
                    context.Response.Redirect("/");
                    return Task.CompletedTask;
                },
                OnRemoteFailure = (ctx) =>
                {
                    if (ctx.Failure?.Message == "Correlation failed.")
                    {
                        ctx.Response.Redirect("/");
                        ctx.HandleResponse();
                    }

                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = (ctx) => {
                    ctx.Response.Redirect("/");
                    ctx.HandleResponse();
                    return Task.CompletedTask;
                }
            };
        });

    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders =
            ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    });

    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IReservoirService, ReservoirService>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IReservoirRepository, ReservoirRepository>();
    builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();
    builder.Services.AddScoped<IOpenXMLUtilitiesService, OpenXMLUtilitiesService>();
    builder.Services.AddScoped<CustomErrorBoundary>();
    builder.Services.AddDataProtection()
        .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
        {
            EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
            ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
        });
    builder.Services.AddScoped<INotification, RaceNotification>();

    var app = builder.Build();
    app.UseForwardedHeaders();
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
    app.UseHttpsRedirection();

    app.UseCookiePolicy(new CookiePolicyOptions
    {
        MinimumSameSitePolicy = SameSiteMode.Lax
    });

    app.UseStaticFiles();

    app.UseAntiforgery();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode();
    //app.MapBlazorHub(options =>
    //{
    //    options.CloseOnAuthenticationExpiration = true;
    //});
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
