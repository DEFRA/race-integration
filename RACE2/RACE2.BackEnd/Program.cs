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
builder.Services
    .AddGraphQLServer()
    .AddTypes()
    .AddType<UploadType>()
    .AddMutationConventions();
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
var app = builder.Build();

app.UseCors("CorsPolicy");
app.UseStaticFiles();
app.MapGraphQL();

app.Run();
