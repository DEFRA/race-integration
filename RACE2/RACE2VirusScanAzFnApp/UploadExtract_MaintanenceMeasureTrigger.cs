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
                RACE2.DataModel.Action _action = new RACE2.DataModel.Action();
                Comment _comment = new Comment();
                _action.Reference = change.Item.Reference;
                _action.Description = change.Item.Action;
                _action.ReservoirId = reservoirSubmission.ReservoirId;
                _comment.CommentText = change.Item.Comment;
                _comment.IsQualityCheckRequired = change.Item.MergedComment;
                _comment.CreatedByUserId = reservoirSubmission.SubmittedByUserId;
                _comment.RelatesToRecordId = 1;

               
                int result = await _reservoirService.InsertMaintenanceMeasureFromExtract(_action,_comment);

            }
        }
    }
  
}
