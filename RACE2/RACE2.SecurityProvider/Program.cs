using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;
using RACE2.DataAccess;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.Logging;
using RACE2.Logging.Service;
using RACE2.SecurityProvider;
using RACE2.SecurityProvider.UtilityClasses;
using RACE2.SecurityProvider.UtilityClasses.CompanyEmployees.OAuth.Extensions;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

//((IConfigurationBuilder)builder.Configuration).Sources.Clear();
//((IConfigurationBuilder)builder.Configuration)
//    //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//    //.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
//    .AddEnvironmentVariables();

////var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
////var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
//var env = builder.Configuration["ASPNETCORE_ENVIRONMENT"];
//var isDevelopment = (env == "Development");
//builder.WebHost.UseUrls(new[] { builder.Configuration["ASPNETCORET_URLS"] });

//if (isDevelopment)
//{
//    //builder.WebHost.UseUrls(new[] { builder.Configuration["ASPNETCORET_URLS"] });
//    //    builder.WebHost.ConfigureKestrel(serverOptions =>
//    //    {
//    //        serverOptions.ListenAnyIP(5010, listenOptions => { });
//    //    });
//}

// Add services to the container.

//var blazorClientURL = builder.Configuration["ApplicationSettings:RACE2FrontEndURL"];
//var webapiURL = builder.Configuration["ApplicationSettings:RACE2WebApiURL"];
//var securityProviderURL = builder.Configuration["ApplicationSettings:RACE2SecurityProviderURL"];
//var sqlConnectionString = builder.Configuration["SqlConnection"];

// Load configuration from Azure App Configuration
//string appconfigConnectionString = builder.Configuration["AzureAppConfigConnString"];
//builder.Configuration.AddAzureAppConfiguration(appconfigConnectionString);
// Load configuration from Azure KeyVault Secret
//var secretClient = new SecretClient(new Uri(builder.Configuration["AzureVaultURL"]), new DefaultAzureCredential());
//var secret = await secretClient.GetSecretAsync("SqlServerConnString");
//var sqlConnectionString = secret.Value.Value;

builder.Configuration.AddAzureAppConfiguration(options =>
{
    var connectionString = builder.Configuration["AZURE_APPCONFIGURATION_CONNECTIONSTRING"];
    var azureAppConfigUrl = builder.Configuration["AzureAppConfigURL"];
    var credential = new DefaultAzureCredential();
    
    //options.Connect(connectionString);      
    options.Connect(new Uri(azureAppConfigUrl),credential);

    options.ConfigureKeyVault(options =>
    {
        options.SetCredential(credential);
    });

    // Load configuration values with no label
    //options.Select(KeyFilter.Any, LabelFilter.Null);
    options.ConfigureRefresh(refreshOptions =>
            refreshOptions.Register("refreshAll", refreshAll: true));
    options.Select(KeyFilter.Any, LabelFilter.Null);
    // Override with any configuration values specific to current hosting env
    options.Select(KeyFilter.Any, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
});
var blazorClientURL= builder.Configuration["RACE2FrontEndURL"];
var webapiURL = builder.Configuration["RACE2WebApiURL"];
var securityProviderURL = builder.Configuration["RACE2SecurityProviderURL"];
var sqlConnectionString = builder.Configuration["SqlConnectionString"];

// Add Azure App Configuration and feature management services to the container.
builder.Services.AddAzureAppConfiguration()
                .AddFeatureManagement();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(sqlConnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<UserDetail>(options => options.SignIn.RequireConfirmedAccount = true)
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
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();

HostingExtensions.InitializeDatabase(app, blazorClientURL, webapiURL);
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax
});
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
