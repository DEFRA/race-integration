using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.Sql;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace RACE2.TrackChangeHistory
{
    public class SqlTriggerBinding
    {
        // Visit https://aka.ms/sqltrigger to learn how to use this trigger binding
        [FunctionName("RAW_StatementDetailsTrigger")]
        public void Run(
                [SqlTrigger("", "")] IReadOnlyList<SqlChange<ToDoItem>> changes,
                ILogger log)
        {
            log.LogInformation("SQL Changes: " + JsonConvert.SerializeObject(changes));

        }
    }

    public class ToDoItem
    {
        public string Id { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
    }
}
