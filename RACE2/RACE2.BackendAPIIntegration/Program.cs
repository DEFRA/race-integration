using Microsoft.OpenApi.Models;
using RACE2.BackendAPIIntegration.Authentication;
using RACE2.DataAccess.Repository;
using RACE2.Services;
using Microsoft.Extensions.Configuration;
using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.EntityFrameworkCore;
using RACE2.BackendAPIIntegration.Data;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Microsoft.ApplicationInsights.Extensibility;

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

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ServiceName", Version = "1" });
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Name = "x-api-key",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "Authorization by x-api-key inside request's header",
        Scheme = "ApiKeyScheme"
    });

    var key = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };

    var requirement = new OpenApiSecurityRequirement { { key, new List<string>() } };
    c.AddSecurityRequirement(requirement);
});
var appinsightsConnString = builder.Configuration["AppInsightsConnectionString"];
var sqlConnectionString = builder.Configuration["SqlConnectionString"];

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
builder.Services.AddScoped<ApiKeyAuthFilter>();

builder.Services.AddTransient<IRACEIntegrationRepository, RACEIntegrationRepository>();
builder.Services.AddTransient<IRACEIntegrationService, RACEIntegrationService>();

var connectionString = builder.Configuration["SqlConnectionString"];
builder.Services.AddDbContext<Pocracinfdb1402Context>(option => option.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
