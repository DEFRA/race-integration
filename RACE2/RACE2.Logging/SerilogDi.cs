using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Logging
{
    public static class SerilogDi
    {
        public static IHostBuilder InjectSerilog(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                loggerConfiguration.MinimumLevel.Debug()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                        .Enrich.FromLogContext();
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
