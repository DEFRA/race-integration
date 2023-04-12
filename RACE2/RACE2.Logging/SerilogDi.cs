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
using Serilog.Sinks.AzureBlobStorage;

namespace RACE2.Logging
{
    public static class SerilogDi
    {
        public static IConfiguration _configuration;
        public static IHostBuilder InjectSerilog(this IHostBuilder hostBuilder)
        {
           // _configuration = configuration;
            
              hostBuilder.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                var storageaccountstring = _configuration["StorageAccountConnString"];

                   var serviceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=race2storageaccount;AccountKey=+voxyaI7i37XXY89mgL3FAg/1JhvSezh1ENdokcV5GMwCOycBYNfYY15aUak3iD+DMvG0Z4kOc6u+ASt0Rq3ZA==;EndpointSuffix=core.windows.net");
               // var serviceClient = new BlobServiceClient(_configuration["StorageAccountConnString"]);


                //  loggerConfiguration.ReadFrom.Configuration(_configuration);



                loggerConfiguration
                    .WriteTo.AzureBlobStorage(serviceClient, restrictedToMinimumLevel: LogEventLevel.Information,
        storageContainerName: "race2appauditlog",
                storageFileName: "WebLog.log",
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                writeInBatches: true, 
                period: TimeSpan.FromSeconds(30),
                batchPostingLimit: 50
                );


      
        });

            return hostBuilder;
        }
    }
}


