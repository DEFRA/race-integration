using Azure.Core;
using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using RACE2.DataAccess;
using RACE2.DataModel;
using RACE2.SecurityProvider;
using RACE2.SecurityProvider.UtilityClasses;
using RACE2.SecurityProvider.UtilityClasses.CompanyEmployees.OAuth.Extensions;
using System.IO.Packaging;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var appConfigEndpoint = builder.Configuration["AZURE_APP_CONFIGURATION_ENDPOINT"];
var userAssignedIdentityClientId = builder.Configuration["AZURE_USER_ASSIGNED_IDENTITY_CLIENT_ID"];

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true)
    //.AddEnvironmentVariables()
    //.AddAzureAppConfiguration(options =>
    //{
    //    //var appConfigEndpoint = System.Environment.GetEnvironmentVariable("AZURE_APP_CONFIGURATION_ENDPOINT");
    //    //var userAssignedIdentityClientId = System.Environment.GetEnvironmentVariable("AZURE_USER_ASSIGNED_IDENTITY_CLIENT_ID");
    //    var endpoint = new Uri(appConfigEndpoint);
    //    // Create the token credential instance with the client id of the Managed Identity
    //    TokenCredential credentials;
    //    if (builder.Environment.IsDevelopment())
    //    {
    //        credentials = new DefaultAzureCredential(new DefaultAzureCredentialOptions
    //        {
    //            ManagedIdentityClientId = userAssignedIdentityClientId
    //        });
    //    }
    //    else
    //    {
    //        credentials = new ManagedIdentityCredential(userAssignedIdentityClientId);
    //    }
    //    options
    //        // Use managed identity to access app configuration
    //        .Connect(endpoint, credentials)
    //        // Setup dynamic refresh
    //        //.ConfigureRefresh(refreshOpt =>
    //        //{
    //        //    refreshOpt.SetCacheExpiration(TimeSpan.FromDays(1));
    //        //})
    //        // Configure Azure Key Vault with Managed Identity
    //        .ConfigureKeyVault(vaultOpt =>
    //        {
    //            vaultOpt.SetCredential(credentials);
    //            vaultOpt.SetSecretRefreshInterval(TimeSpan.FromHours(12));
    //        });
    //})
    ;

string RACE2FrontEndURL = builder.Configuration["RACE2FrontEndURL"];

var migrationsAssembly = typeof(Program).Assembly.GetName().Name;

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var connectionString = builder.Configuration["SqlConnection"];

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<UserDetail>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

builder.Services.AddIdentityServer()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
}
HostingExtensions.InitializeDatabase(app);
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
