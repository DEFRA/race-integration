using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace RACE2.Logging
{
    public static class SerilogDi
    {
        public static IHostBuilder InjectSerilog(this IHostBuilder hostBuilder)
        {
          //  var connectionString = " DefaultEndpointsProtocol = https; AccountName = race2storageaccount; AccountKey = +voxyaI7i37XXY89mgL3FAg / 1JhvSezh1ENdokcV5GMwCOycBYNfYY15aUak3iD + DMvG0Z4kOc6u + ASt0Rq3ZA ==; EndpointSuffix = core.windows.net";
              //  "DefaultEndpointsProtocol=https;AccountName=xxxxxxx;AccountKey=xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx;EndpointSuffix=core.windows.net";

            hostBuilder.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                
                IConfiguration _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../appsettings.json").Build();
                //  var connectionString = _configuration.GetSection()
                //   var cloudAccount = .Parse("ConnectionString");

                //  IConfigurationRoot config = builder.Build();

                //string containerName = "race2appauditlog";
                //var serviceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=race2storageaccount;AccountKey=+voxyaI7i37XXY89mgL3FAg/1JhvSezh1ENdokcV5GMwCOycBYNfYY15aUak3iD+DMvG0Z4kOc6u+ASt0Rq3ZA==;EndpointSuffix=core.windows.net");
                //var containerClient = serviceClient.GetBlobContainerClient(containerName);
                //var blobClient = containerClient.GetBlobClient(containerName);

                //var log = new LoggerConfiguration()
                //    .WriteTo.AzureBlobStorage(serviceClient,LogEventLevel.Verbose,containerName,"RACELOg.txt");
                //    //.CreateLogger();
                loggerConfiguration.ReadFrom.Configuration(_configuration);

                //builder.Host.UseSerilog((ctx, lc) => {
                //    lc.ReadFrom.Configuration(ctx.Configuration);
                //});

                //loggerConfiguration.ReadFrom.Configuration(_configuration);
                //  loggerConfiguration.WriteTo.AddBlobServiceClient()

                //loggerConfiguration.MinimumLevel.Debug()
                //        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                //        .Enrich.FromLogContext()
                //        .WriteTo.AzureBlobStorage(connectionString: connectionString);
                //restrictedToMinimumLevel: LogEventLevel.Warning,
                //storageFileName: "{yyyy}/{MM}-{dd}/WebLog.txt",
                //outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                //storageContainerName: "race2appauditlog");


                //.WriteTo.File(path: Path.Combine(Environment.CurrentDirectory, "Logs", "log.txt"),
                //    rollOnFileSizeLimit: true,
                //    retainedFileCountLimit: 20,
                //    rollingInterval: RollingInterval.Day,
                //    fileSizeLimitBytes: 10000
                //    )
                //.WriteTo.Console();
            });

            return hostBuilder;
        }
    }
}


