using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RACE2.Services;
using Microsoft.Extensions.Configuration;
using RACE2.DataModel;
using RACE2.Dto;

namespace RACE2VirusScanAzFnApp
{
    public class UploadExtract_MaintanenceMeasureTrigger
    {
        private readonly ILogger<UploadExtract_ActionSummaryTrigger> _logger;
        private readonly IConfiguration _config;
        private readonly IReservoirService _reservoirService;

        public UploadExtract_MaintanenceMeasureTrigger(ILogger<UploadExtract_ActionSummaryTrigger> logger, IConfiguration config, IReservoirService reservoirService)
        {
            _logger = logger;
            _config = config;
            _reservoirService = reservoirService;
        }

        // Visit https://aka.ms/sqltrigger to learn how to use this trigger binding
        [Function("UploadExtract_MaintanenceMeasureTrigger")]
        public async Task Run(
            [SqlTrigger("RAW_MaintenanceMeasures", "SqlServerConnectionString")] IReadOnlyList<SqlChange<RAW_MaintenanceMeasures>> changes,
                FunctionContext context)
        {
            ReservoirSubmissionDTO reservoirSubmission = new ReservoirSubmissionDTO();
            ReservoirDetailsChangeHistory changeHistory = new ReservoirDetailsChangeHistory();
            List<ReservoirDetailsChangeHistory> reservoirDetailsChangeHistory = new List<ReservoirDetailsChangeHistory>();
            foreach (var change in changes)
            {
                _logger.LogInformation("SQL Changes: " + JsonConvert.SerializeObject(changes));
                string[] subs = change.Item.DocumentName.Split('_');
                reservoirSubmission = await _reservoirService.GetReservoirUserIdbySubRef(subs[0].ToString());

            }
        }
    }
  
}
