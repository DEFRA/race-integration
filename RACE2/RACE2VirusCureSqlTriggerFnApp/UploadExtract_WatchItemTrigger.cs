using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace RACE2VirusCureSqlTriggerFnApp
{
    public class UploadExtract_WatchItemTrigger
    {
        private readonly ILogger _logger;

        public UploadExtract_WatchItemTrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<UploadExtract_WatchItemTrigger>();
        }

        // Visit https://aka.ms/sqltrigger to learn how to use this trigger binding
        [Function("UploadExtract_WatchItemTrigger")]
        public void Run(
            [SqlTrigger("[dbo].[RAW_WatchItems]", "SqlServerConnectionString")] IReadOnlyList<SqlChange<ToDoItem>> changes,
                FunctionContext context)
        {
            _logger.LogInformation("SQL Changes: " + JsonConvert.SerializeObject(changes));

        }
    }   
}
