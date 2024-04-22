
//using RACE2.BackendAPIIntegration.Services;
using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using RACE2.DataAccess.Repository;

using RACE2.Services;

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
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<DbContextClass>();
//builder.Services.AddTransient<IFileService, FileService>();
//builder.Services.AddTransient<IFileRepository, FileRepository>();
builder.Services.AddTransient<IRACEIntegrationRepository, RACEIntegrationRepository>();
builder.Services.AddTransient<IRACEIntegrationService , RACEIntegrationService>();

builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = builder.Configuration["AppInsightsConnectionString"];
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();
app.Run();































//using Microsoft.OpenApi.Models;
//using Microsoft.Extensions.Configuration.AzureAppConfiguration;
//using RACE2.BackendAPIIntegration.Authentication;
//using Azure.Identity;
//using RACE2.DataAccess.Repository;
//using RACE2.Services;
//using RACE2.BackendAPIIntegration.Services;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

////builder.Configuration.AddAzureAppConfiguration(options =>
////{
////    //var connectionString = builder.Configuration["AZURE_APPCONFIGURATION_CONNECTIONSTRING"];
////    var azureAppConfigUrl = builder.Configuration["AzureAppConfigURL"];
////    var azureTenantId = builder.Configuration["AZURE_TENANT_ID"];
////    var managedIdenityClientId = builder.Configuration["ManagedIdenityClientId"];
////    var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions 
////    { TenantId = azureTenantId, ManagedIdentityClientId = managedIdenityClientId, VisualStudioTenantId = azureTenantId });

////    //options.Connect(connectionString)      
////    options.Connect(new Uri(azureAppConfigUrl), credential)
////    .ConfigureKeyVault(options =>
////    {
////        options.SetCredential(credential);
////    })
////    .ConfigureRefresh(refreshOptions =>
////            refreshOptions.Register("refreshAll", refreshAll: true))
////    .Select(KeyFilter.Any, LabelFilter.Null)
////    // Override with any configuration values specific to current hosting env
////    .Select(KeyFilter.Any, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
////    .UseFeatureFlags();
////});


//var blazorClientURL = builder.Configuration["RACE2FrontEndURL"];
//var RACE2WebApiURL = builder.Configuration["RACE2WebApiURL"];
//var RACE2IDPURL = builder.Configuration["RACE2SecurityProviderURL"];
//var clientSecret = builder.Configuration["ClientSecret"];
//var appinsightsConnString = builder.Configuration["AppInsightsConnectionString"];
//var sqlConnectionString = "Server=tcp:tstracinfss1401.database.windows.net,1433;Initial Catalog=TSTRACINFDB1401;Persist Security Info=False;User ID=race2admin;Password=Race2Password123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"; //builder.Configuration["SqlConnectionString"];

//builder.Services.AddTransient<IUserRepository, UserRepository>();
//builder.Services.AddTransient<IUserService, UserService>();
//builder.Services.AddTransient<IReservoirRepository, ReservoirRepository>();
//builder.Services.AddTransient<IReservoirService, ReservoirService>();
//builder.Services.AddTransient<IRACEIntegrationRepository, RACEIntegrationRepository>();
//builder.Services.AddTransient<IRACEIntegrationService, RACEIntegrationService>();
//builder.Services.AddTransient<IFileService,FileService>();
//builder.Services.AddTransient<IFileRepository,FileRepository>();


//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ServiceName", Version = "1" });
//    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
//    {
//        Name = "x-api-key",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        Description = "Authorization by x-api-key inside request's header",
//        Scheme = "ApiKeyScheme"
//    });

//    var key = new OpenApiSecurityScheme()
//    {
//        Reference = new OpenApiReference
//        {
//            Type = ReferenceType.SecurityScheme,
//            Id = "ApiKey"
//        },
//        In = ParameterLocation.Header
//    };

//    var requirement = new OpenApiSecurityRequirement { { key, new List<string>() } };
//    c.AddSecurityRequirement(requirement);
//});

//builder.Services.AddScoped<ApiKeyAuthFilter>();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

////app.UseStaticFiles();

//app.UseRouting();

//app.MapControllers();

//app.Run();
