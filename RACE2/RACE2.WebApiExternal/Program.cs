using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Microsoft.ApplicationInsights.Extensibility;
Log.Logger = new LoggerConfiguration()
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

    var blazorClientURL = builder.Configuration["RACE2FrontEndURL"];
    var RACE2WebApiURL = builder.Configuration["RACE2WebApiURL"];
    var RACE2IDPURL = builder.Configuration["RACE2SecurityProviderURL"];
    var clientSecret = builder.Configuration["ClientSecret"];
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

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var authority = builder.Configuration["RACE2SecurityProviderURL"];
    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
    builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.Authority = authority;
                    jwtBearerOptions.RequireHttpsMetadata = false;
                    jwtBearerOptions.Audience = "race2WebApi";
                    jwtBearerOptions.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            RoleClaimType = "role"
                        };
                    jwtBearerOptions.TokenValidationParameters.ValidateAudience = true;
                    jwtBearerOptions.TokenValidationParameters.ValidateIssuer = true;
                    jwtBearerOptions.TokenValidationParameters.ValidateIssuerSigningKey = true;
                });
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
    app.UseSerilogRequestLogging();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

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