using System;
using Microsoft.Azure.Functions.Worker;
using System;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RACE2.Services;
using Microsoft.Extensions.Configuration;
using RACE2.DataModel;
using RACE2.Dto;

namespace RACE2.VirusCureSqlTriggerFnApp
{
    public class UploadExtract_MaintanenceMeasureTrigger
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private readonly IReservoirService _reservoirService;
        public UploadExtract_MaintanenceMeasureTrigger(ILoggerFactory loggerFactory, IConfiguration config, IReservoirService reservoirService)
        {
            _logger = loggerFactory.CreateLogger<UploadExtract_MaintanenceMeasureTrigger>();
            _config = config;
            _reservoirService = reservoirService;
        }

        // Visit https://aka.ms/sqltrigger to learn how to use this trigger binding
        [Function("UploadExtract_MaintanenceMeasureTrigger")]
        public async Task Run(
            [SqlTrigger("[dbo].[RAW_MaintenanceMeasures]", "SqlServerConnectionString")] IReadOnlyList<SqlChange<RAW_MaintenanceMeasures>> changes,
                FunctionContext context)
        {
            ReservoirSubmissionDTO reservoirSubmission = new ReservoirSubmissionDTO();
            ActionsChangeHistory changeHistory = new ActionsChangeHistory();
            foreach (var change in changes)
            {
                int result;
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
                _comment.SourceSubmissionId = reservoirSubmission.SubmissionId;
                _action.CreatedDate = Convert.ToDateTime(change.Item.LastModifiedDateTime);

                RACE2.DataModel.Action _existingWatchItems = await _reservoirService.GetActionsListByReservoirIdAndCategory(_action.ReservoirId, 2, _action.Reference);
                if (_existingWatchItems == null)
                    result = await _reservoirService.InsertorUpdateMaintenanceMeasureFromExtract(_action, _comment);
                else
                {
                    List<ActionsChangeHistory> _actionChangeHistory = new List<ActionsChangeHistory>();

                    if (_action.Reference != _existingWatchItems.Reference)
                    {
                        changeHistory = AddHistory(_existingWatchItems.Id, _existingWatchItems.Reference, _action.Reference, "Reference", reservoirSubmission, _action.CreatedDate);
                        _actionChangeHistory.Add(changeHistory);
                    }

                    if (_action.Description != _existingWatchItems.Description)
                    {
                        changeHistory = AddHistory(_existingWatchItems.Id, _existingWatchItems.Description, _action.Description, "Description", reservoirSubmission, _action.CreatedDate);
                        _actionChangeHistory.Add(changeHistory);
                    }


                    int history = await _reservoirService.InsertActionChangeHistory(_actionChangeHistory);
                    result = await _reservoirService.InsertorUpdateMaintenanceMeasureFromExtract(_action, _comment);
                }

            }
        }

        public static ActionsChangeHistory AddHistory(int Actionid, string OldValue, String NewValue, string FieldName, ReservoirSubmissionDTO submissionDetails, DateTime changedatetime)
        {
            ActionsChangeHistory changeHistory = new ActionsChangeHistory();
            //  changeHistory = null;
            if ((!String.IsNullOrEmpty(NewValue)) && (!String.IsNullOrEmpty(OldValue)))
            {
                if (NewValue != OldValue)
                {
                    changeHistory.OldValue = OldValue;
                    changeHistory.NewValue = NewValue;
                    changeHistory.FieldName = FieldName;
                    changeHistory.IsBackEndChange = false;
                    changeHistory.ChangeByUserId = submissionDetails.SubmittedByUserId;
                    changeHistory.SourceSubmissionId = submissionDetails.SubmissionId;
                    changeHistory.ReservoirId = submissionDetails.ReservoirId;
                    changeHistory.ActionId = Actionid;
                    changeHistory.ChangeDateTime = changedatetime;
                }
                else
                    return null;
            }
            else if ((String.IsNullOrEmpty(NewValue)) || (String.IsNullOrEmpty(OldValue)))
            {
                changeHistory.OldValue = (OldValue == null) ? null : OldValue.ToString();
                changeHistory.NewValue = (NewValue == null) ? null : NewValue.ToString(); ;
                changeHistory.FieldName = FieldName;
                changeHistory.IsBackEndChange = false;
                changeHistory.ChangeDateTime = changedatetime;
                changeHistory.ChangeByUserId = submissionDetails.SubmittedByUserId;
                changeHistory.SourceSubmissionId = submissionDetails.SubmissionId;
                changeHistory.ReservoirId = submissionDetails.ReservoirId;
                changeHistory.ActionId = Actionid;
            }
            else
            {
                return null;
            }
            return changeHistory;
        }
    }
}
