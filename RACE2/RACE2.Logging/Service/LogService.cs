using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RACE2.Logging.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Serilog;
using Serilog.Core;

namespace RACE2.Logging.Service
{

    public class LogService :ILogService
    {
       // private readonly ILoggerFactory _loggerFactory;
        //private ILogger _logger;




        IConfiguration _configuration;
        public LogService(IConfiguration configuration)
        {
            _configuration = configuration;
            //_configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../appsettings.json").Build();
           Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(_configuration)
                //.WriteTo.File(path: Path.Combine(Environment.CurrentDirectory, "RaceLogs", "log.txt"),
                //            rollOnFileSizeLimit: true,
                //            retainedFileCountLimit: 20,
                //            rollingInterval: RollingInterval.Day,
                //            fileSizeLimitBytes: 10000
                //            )
                .CreateLogger();
            
        }
        //void Log(string category, LogMessage message, Action<ILogger, string> func)
        //{
        //    _logger = _loggerFactory.CreateLogger(category);
        //    var actualError = ConstructMessage(message);
        //    func(_logger, actualError);
        //}

        static string ConstructMessage(LogMessage message)
        {
            JObject log = new()
            {
                ["user"] = message.UserId,
                ["message"] = message.Message,
                ["logTime"] = message.LogTime
            };

            if (message.ExceptionInfo != null)
            {
                log["stack"] = JsonConvert.SerializeObject(message.ExceptionInfo);
            }

            return log.ToString();
        }

        //public void Critical(string category, LogMessage message)
        //{
        //    static void func(ILogger _logger, string actualError)
        //    {
        //        _logger.LogCritical(actualError);
        //    }

        //    Log(category, message, func);
        //}

        //public void Error(string category, LogMessage message)
        //{
        //    static void func(ILogger _logger, string actualError)
        //    {
        //        _logger.LogError(actualError);
        //    }

        //    Log(category, message, func);
        //}

        //public void Error(string category, LogMessage message, Exception ex)
        //{
        //    message.ExceptionInfo = ex;

        //    static void func(ILogger _logger, string actualError)
        //    {
        //        _logger.LogError(actualError);
        //    }

        //    Log(category, message, func);
        //}

        //public void Warning(string category, LogMessage message)
        //{
        //    static void func(ILogger _logger, string actualError)
        //    {
        //        _logger.LogWarning(actualError);
        //    }

        //    Log(category, message, func);
        //}

        public void Write(string message)
        {
            Log.Information(message);
        }

        public void Error(Exception exception, string message)
        {
            Log.Error(exception, message);
        }
    }
}
