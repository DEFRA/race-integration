using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace RACE2VirusCureSqlTriggerFnApp
{
    public class UploadExtract_MIOSTrigger
    {
        private readonly ILogger _logger;

        public UploadExtract_MIOSTrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<UploadExtract_MIOSTrigger>();
        }

        // Visit https://aka.ms/sqltrigger to learn how to use this trigger binding
        [Function("UploadExtract_MIOSTrigger")]
        public void Run(
            [SqlTrigger("[dbo].[RAW_MIOS]", "SqlServerConnectionString")] IReadOnlyList<SqlChange<ToDoItem>> changes,
                FunctionContext context)
        {
            _logger.LogInformation("SQL Changes: " + JsonConvert.SerializeObject(changes));

        }
    }    
}
