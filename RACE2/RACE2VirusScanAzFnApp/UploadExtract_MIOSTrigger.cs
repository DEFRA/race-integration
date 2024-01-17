using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RACE2.DataModel;
using Microsoft.Extensions.Configuration;
using RACE2.Services;
using RACE2.Dto;

namespace RACE2VirusScanAzFnApp
{
    public class UploadExtract_MIOSTrigger
    {
        private readonly ILogger<UploadExtract_MIOSTrigger> _logger;
        private readonly IConfiguration _config;
        private readonly IReservoirService _reservoirService;

        public UploadExtract_MIOSTrigger(ILogger<UploadExtract_MIOSTrigger> logger, IConfiguration config, IReservoirService reservoirService)
        {
            _logger = logger;
            _config = config;
            _reservoirService = reservoirService;
        }

        // Visit https://aka.ms/sqltrigger to learn how to use this trigger binding
        [Function("UploadExtract_MIOSTrigger")]
        public async Task Run(
            [SqlTrigger("RAW_MIOS", "SqlServerConnectionString")] IReadOnlyList<SqlChange<RAW_MIOS>> changes,
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
                SafetyMeasure safetyMeasure = new SafetyMeasure();
                Comment _comment = new Comment();
                safetyMeasure.Reference = change.Item.Reference;
                safetyMeasure.Description = change.Item.Action;
                safetyMeasure.ReservoirId = reservoirSubmission.ReservoirId;
                safetyMeasure.Status = change.Item.Outstanding;
                safetyMeasure.TargetDate = Convert.ToDateTime(change.Item.Deadline);
                _comment.CommentText = change.Item.Comment;
                _comment.IsQualityCheckRequired = change.Item.MergedComment;
                _comment.CreatedByUserId = reservoirSubmission.SubmittedByUserId;
                _comment.RelatesToRecordId = 1;


                int result = await _reservoirService.InsertSafetyMeasuresFromExtract(safetyMeasure, _comment);

            }

        }
    }

   
}
