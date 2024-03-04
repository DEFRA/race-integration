using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace RACE2.VirusCureSqlTriggerFnApp
{
    public class UploadExtract_ActionSummaryTrigger
    {
        private readonly ILogger _logger;

        public UploadExtract_ActionSummaryTrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<UploadExtract_ActionSummaryTrigger>();
        }

        // Visit https://aka.ms/sqltrigger to learn how to use this trigger binding
        [Function("UploadExtract_ActionSummaryTrigger")]
        public void Run(
            [SqlTrigger("[dbo].[RAW_ActionSummary]", "SqlServerConnectionString")] IReadOnlyList<SqlChange<ToDoItem>> changes,
                FunctionContext context)
        {
            _logger.LogInformation("SQL Changes: " + JsonConvert.SerializeObject(changes));

        }
    }
   
}
