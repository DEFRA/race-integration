using System;
using Microsoft.Azure.Functions.Worker;
using System;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RACE2.DataModel;
using Microsoft.Extensions.Configuration;
using RACE2.Services;
using RACE2.Dto;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System.Reflection;
using RACE2.Common;
using Grpc.Core;

namespace RACE2.MultipleTriggerAzureFunctionApp
{
    public class UploadExtract_MIOSTrigger
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private readonly IReservoirService _reservoirService;

        public UploadExtract_MIOSTrigger(ILoggerFactory loggerFactory, IConfiguration config, IReservoirService reservoirService)
        {
            _logger = loggerFactory.CreateLogger<UploadExtract_MIOSTrigger>();
            _config = config;
            _reservoirService = reservoirService;
        }

        // Visit https://aka.ms/sqltrigger to learn how to use this trigger binding
        [Function("UploadExtract_MIOSTrigger")]
        public async Task Run(
            [SqlTrigger("[dbo].[RAW_MIOS]", "SqlServerConnectionString")] IReadOnlyList<SqlChange<RAW_MIOS>> changes,
                FunctionContext context)
        {
            ReservoirSubmissionDTO reservoirSubmission = new ReservoirSubmissionDTO();
            List<SafetyMeasure> _extractedSafetyMeasureList = new List<SafetyMeasure>();
            SafetyMeasuresChangeHistory changeHistory = null;
            CommentsChangeHistory commentsChangeHistory = new CommentsChangeHistory();
            foreach (var change in changes)
            {
                int result;
                _logger.LogInformation("SQL Changes: " + JsonConvert.SerializeObject(changes));
                string[] subs = change.Item.DocumentName.Split('_');
                reservoirSubmission = await _reservoirService.GetReservoirUserIdbySubRef(subs[0].ToString());

                SafetyMeasure safetyMeasure = new SafetyMeasure();
                Comment _comment = new Comment();
                safetyMeasure.Reference = change.Item.Reference;
                safetyMeasure.Description = change.Item.Action;
                safetyMeasure.ReservoirId = reservoirSubmission.ReservoirId;
                if (change.Item.Outstanding == "Yes")
                    safetyMeasure.Status = "Open";
                if (change.Item.Outstanding == "No")
                    safetyMeasure.Status = "Closed";
                safetyMeasure.TargetDate = Convert.ToDateTime(change.Item.Deadline);
                safetyMeasure.CreatedDate = Convert.ToDateTime(change.Item.LastModifiedDateTime);
                _comment.CommentText = change.Item.Comment;
                _comment.IsQualityCheckRequired = change.Item.MergedComment;
                _comment.CreatedByUserId = reservoirSubmission.SubmittedByUserId;
                _comment.RelatesToRecordId = 1;
                _comment.SourceSubmissionId = reservoirSubmission.SubmissionId;
                _extractedSafetyMeasureList.Add(safetyMeasure);
                // int result  = await CompareMIOSListWithDB(_extractedSafetyMeasureList);
                SafetyMeasure _existingsafetyMeasure = await _reservoirService.GetSafetyMeasuresByReservoir(safetyMeasure.ReservoirId, safetyMeasure.Reference);
                Comment _existingComments = await _reservoirService.GetExisitngComments("SafetyMeasure", _existingsafetyMeasure.Id);

                if (_existingsafetyMeasure == null)
                    result = await _reservoirService.InsertorUpdateSafetyMeasuresFromExtract(safetyMeasure, _comment);
                else
                {
                    List<SafetyMeasuresChangeHistory> _UpdatedChangeHistory = new List<SafetyMeasuresChangeHistory>();
                    List<CommentsChangeHistory> _commentsChangeHistory = new List<CommentsChangeHistory>();


                    if (safetyMeasure.Reference != _existingsafetyMeasure.Reference)
                    {
                        changeHistory = AddHistory(_existingsafetyMeasure.Id, _existingsafetyMeasure.Reference, safetyMeasure.Reference, "Reference", reservoirSubmission, safetyMeasure.CreatedDate);
                        _UpdatedChangeHistory.Add(changeHistory);
                    }

                    if (safetyMeasure.Description != _existingsafetyMeasure.Description)
                    {
                        changeHistory = AddHistory(_existingsafetyMeasure.Id, _existingsafetyMeasure.Description, safetyMeasure.Description, "Description", reservoirSubmission, safetyMeasure.CreatedDate);
                        _UpdatedChangeHistory.Add(changeHistory);
                    }
                    if (safetyMeasure.TargetDate != _existingsafetyMeasure.TargetDate)
                    {
                        changeHistory = AddHistory(_existingsafetyMeasure.Id, Convert.ToString(_existingsafetyMeasure.TargetDate), Convert.ToString(safetyMeasure.TargetDate), "TargetDate", reservoirSubmission, safetyMeasure.CreatedDate);
                        _UpdatedChangeHistory.Add(changeHistory);
                    }
                    if (safetyMeasure.Status != _existingsafetyMeasure.Status)
                    {
                        changeHistory = AddHistory(_existingsafetyMeasure.Id, Convert.ToString(_existingsafetyMeasure.Status), Convert.ToString(safetyMeasure.Status), "Status", reservoirSubmission, safetyMeasure.CreatedDate);
                        _UpdatedChangeHistory.Add(changeHistory);
                    }

                    if (_comment.CommentText != _existingComments.CommentText)
                    {
                        //commentsChangeHistory = AddCommentsHistory(_existingComments.Id, _existingComments.CommentText, _comment.CommentText, "CommentText", reservoirSubmission, _action.CreatedDate);
                        // _commentsChangeHistory.Add(commentsChangeHistory);
                    }

                    int history = await _reservoirService.InsertSafetyMeasureChangeHistory(_UpdatedChangeHistory);
                    result = await _reservoirService.InsertorUpdateSafetyMeasuresFromExtract(safetyMeasure, _comment);

                }

            }

        }


        public static SafetyMeasuresChangeHistory AddHistory(int MeasureId, string OldValue, String NewValue, string FieldName, ReservoirSubmissionDTO submissionDetails, DateTime changeTime)
        {
            SafetyMeasuresChangeHistory changeHistory = new SafetyMeasuresChangeHistory();
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
                    changeHistory.MeasureId = MeasureId;
                    changeHistory.ChangeDateTime = changeTime;
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
                changeHistory.ChangeDateTime = changeTime;
                changeHistory.ChangeByUserId = submissionDetails.SubmittedByUserId;
                changeHistory.SourceSubmissionId = submissionDetails.SubmissionId;
                changeHistory.ReservoirId = submissionDetails.ReservoirId;
                changeHistory.MeasureId = MeasureId;
            }
            else
            {
                return null;
            }
            return changeHistory;
        }       
    }
}
