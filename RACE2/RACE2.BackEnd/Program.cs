using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using RACE2.DataAccess.Repository;
using RACE2.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddAzureAppConfiguration(options =>
{
    var azureAppConfigUrl = builder.Configuration["AzureAppConfigURL"];
    var credential = new DefaultAzureCredential();
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
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IReservoirRepository, ReservoirRepository>();
builder.Services.AddTransient<IReservoirService, ReservoirService>();
builder.Services.AddTransient<IRACEIntegrationRepository, RACEIntegrationRepository>();
builder.Services.AddTransient<IRACEIntegrationService, RACEIntegrationService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

builder.Services
    .AddGraphQLServer()
    .RegisterService<IUserService>()
    .RegisterService<IReservoirService>()
    .RegisterService<IRACEIntegrationService>()
    .AddTypes()
    .AddType<UploadType>()
    .AddMutationConventions();

var app = builder.Build();
app.UseCors("CorsPolicy");
app.MapGraphQL();

app.RunWithGraphQLCommands(args);
