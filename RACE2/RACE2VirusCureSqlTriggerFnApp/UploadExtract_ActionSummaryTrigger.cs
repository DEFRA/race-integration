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

namespace RACE2.MultipleTriggerAzureFunctionApp
{
    public class UploadExtract_ActionSummaryTrigger
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private readonly IReservoirService _reservoirService;

        public UploadExtract_ActionSummaryTrigger(ILoggerFactory loggerFactory, IConfiguration config, IReservoirService reservoirService)
        {
            _logger = loggerFactory.CreateLogger<UploadExtract_ActionSummaryTrigger>();
            _config = config;
            _reservoirService = reservoirService;
        }

        // Visit https://aka.ms/sqltrigger to learn how to use this trigger binding
        [Function("UploadExtract_ActionSummaryTrigger")]
        public async Task Run(
            [SqlTrigger("[dbo].[RAW_ActionSummary]", "SqlServerConnectionString")] IReadOnlyList<SqlChange<RAW_ActionSummary>> changes,
                FunctionContext context)
        {
            _logger.LogInformation("SQL Changes: " + JsonConvert.SerializeObject(changes));

            ReservoirSubmissionDTO reservoirSubmission = new ReservoirSubmissionDTO();
            ActionsChangeHistory changeHistory = new ActionsChangeHistory();
            foreach (var change in changes)
            {
                int result;
                _logger.LogInformation("SQL Changes: " + JsonConvert.SerializeObject(changes));
                string[] subs = change.Item.DocumentName.Split('_');
                reservoirSubmission = await _reservoirService.GetReservoirUserIdbySubRef(subs[0].ToString());
                RACE2.DataModel.Action _action = new RACE2.DataModel.Action();
                _action.Reference = change.Item.Reference;
                _action.Description = change.Item.Action;
                if (change.Item.Mandatory == "Yes") _action.IsMandatory = true; else _action.IsMandatory = Convert.ToBoolean(change.Item.Mandatory);
                if (change.Item.Mandatory == "No") _action.IsMandatory = false; else _action.IsMandatory = Convert.ToBoolean(change.Item.Mandatory);
                // _action.IsMandatory = Convert.ToBoolean(change.Item.Mandatory);
                _action.Priority = change.Item.Priority;
                _action.ReservoirId = reservoirSubmission.ReservoirId;

                RACE2.DataModel.Action _existingActionList = await _reservoirService.GetActionsListByReservoirIdAndCategory(_action.ReservoirId, 2, _action.Reference);
                if (_existingActionList == null)
                    result = await _reservoirService.InsertActionTableFromExtract(_action);
                else
                {
                    List<ActionsChangeHistory> _actionChangeHistory = new List<ActionsChangeHistory>();

                    if (_action.Reference != _existingActionList.Reference)
                    {
                        changeHistory = AddHistory(_existingActionList.Id, _existingActionList.Reference, _action.Reference, "Reference", reservoirSubmission, _action.CreatedDate);
                        _actionChangeHistory.Add(changeHistory);
                    }

                    if (_action.Description != _existingActionList.Description)
                    {
                        changeHistory = AddHistory(_existingActionList.Id, _existingActionList.Description, _action.Description, "Description", reservoirSubmission, _action.CreatedDate);
                        _actionChangeHistory.Add(changeHistory);
                    }

                    if (_action.IsMandatory != _existingActionList.IsMandatory)
                    {
                        changeHistory = AddHistory(_existingActionList.Id, _existingActionList.IsMandatory.ToString(), _action.IsMandatory.ToString(), "IsMandatory", reservoirSubmission, _action.CreatedDate);
                        _actionChangeHistory.Add(changeHistory);
                    }


                    if (_action.Priority != _existingActionList.Priority)
                    {
                        changeHistory = AddHistory(_existingActionList.Id, _existingActionList.Priority, _action.Priority, "Priority", reservoirSubmission, _action.CreatedDate);
                        _actionChangeHistory.Add(changeHistory);
                    }


                    int history = await _reservoirService.InsertActionChangeHistory(_actionChangeHistory);
                    result = await _reservoirService.InsertActionTableFromExtract(_action);
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
   

