using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace RACE2.VirusCureSqlTriggerFnApp
{
    public class UploadDataExtract_StatementDetailsTrigger
    {
        private readonly ILogger _logger;

        public UploadDataExtract_StatementDetailsTrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<UploadDataExtract_StatementDetailsTrigger>();
        }

        // Visit https://aka.ms/sqltrigger to learn how to use this trigger binding
        [Function("UploadDataExtract_StatementDetailsTrigger")]
        public void Run(
            [SqlTrigger("[dbo].[RAW_StatementDetails]", "SqlServerConnectionString")] IReadOnlyList<SqlChange<ToDoItem>> changes,
                FunctionContext context)
        {
            _logger.LogInformation("SQL Changes: " + JsonConvert.SerializeObject(changes));

        }
    }

    public class ToDoItem
    {
        public string Id { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
    }
}
