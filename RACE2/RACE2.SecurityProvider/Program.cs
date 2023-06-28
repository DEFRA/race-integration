using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
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
using System.Configuration;
using System.Data.SqlClient;

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

//var blazorClientURL = "https://race2frontendweb.gentlebush-defe7f09.westeurope.azurecontainerapps.io";
//var webapiURL = "https://race2webapi.gentlebush-defe7f09.westeurope.azurecontainerapps.io/graphql/";
//var securityProviderURL = "https://race2securityprovider.gentlebush-defe7f09.westeurope.azurecontainerapps.io";
//var sqlConnectionString = "Server=tcp:race2sqlserver.database.windows.net,1433;Initial Catalog=RACE2Database;Persist Security Info=False;User ID=race2admin;Password=Race2Password123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
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
builder.Services.AddScoped<INotification, RaceNotification>();
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

// Use Azure App Configuration middleware for dynamic configuration refresh.
app.UseAzureAppConfiguration();

app.UseHttpsRedirection();

HostingExtensions.InitializeDatabase(app, blazorClientURL, webapiURL);//populate initial data

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
