using Microsoft.Extensions.Configuration;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Hosting;
using RACE2.Dto;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using RACE2.DatabaseProvider.Data;
using RACE2.Common;
using Microsoft.Extensions.Logging;
using RACE2.Infrastructure;
using RACE2.DataAccess.Repository;
using RACE2.Services;
using HotChocolate.AspNetCore.Voyager;
//using Microsoft.AspNetCore.Authentication.JwtBearer;

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
builder.Services.AddLoggingServices(builder.Configuration);
builder.Services.AddDbContextServices(builder.Configuration);

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

//builder.Services
//    .AddGraphQLServer()
//    .RegisterService<IUserService>()
//    .RegisterService<IReservoirService>()
//    .RegisterService<IRACEIntegrationService>()
//    .AddTypes()
//    .AddType<UploadType>()
//    .AddMutationConventions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.MapGraphQL();
app.UseVoyager("/graphql", "/graphql-voyager");

app.Run();