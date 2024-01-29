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
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System.Reflection;

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
           
            List<ReservoirDetailsChangeHistory> reservoirDetailsChangeHistory = new List<ReservoirDetailsChangeHistory>();
            List<SafetyMeasure> _extractedSafetyMeasureList = new List<SafetyMeasure>();
            SafetyMeasuresChangeHistory changeHistory = null;
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
                SafetyMeasure _existingsafetyMeasure = await _reservoirService.GetSafetyMeasuresByReservoir(safetyMeasure.ReservoirId,safetyMeasure.Reference);
                if(_existingsafetyMeasure == null)
                  result = await _reservoirService.InsertSafetyMeasuresFromExtract(safetyMeasure, _comment);
                else
                {
                    List<SafetyMeasuresChangeHistory> _UpdatedChangeHistory = new List<SafetyMeasuresChangeHistory>();
                    
                    
                    if (safetyMeasure.Reference != _existingsafetyMeasure.Reference)
                    {
                        changeHistory = AddHistory(_existingsafetyMeasure.Id,_existingsafetyMeasure.Reference, safetyMeasure.Reference, "Reference", reservoirSubmission);
                        _UpdatedChangeHistory.Add(changeHistory);
                    }

                    if(safetyMeasure.Description != _existingsafetyMeasure.Description)
                    {
                        changeHistory = AddHistory(_existingsafetyMeasure.Id,_existingsafetyMeasure.Description, safetyMeasure.Description, "Description", reservoirSubmission);
                        _UpdatedChangeHistory.Add(changeHistory);
                    }
                    if(safetyMeasure.TargetDate != _existingsafetyMeasure.TargetDate)
                    {
                        changeHistory = AddHistory(_existingsafetyMeasure.Id, Convert.ToString(_existingsafetyMeasure.TargetDate), Convert.ToString(safetyMeasure.TargetDate), "TargetDate", reservoirSubmission);
                        _UpdatedChangeHistory.Add(changeHistory);
                    }
                    if(safetyMeasure.Status != _existingsafetyMeasure.Status)
                    {
                        changeHistory = AddHistory(_existingsafetyMeasure.Id, Convert.ToString(_existingsafetyMeasure.Status), Convert.ToString(safetyMeasure.Status), "Status", reservoirSubmission);
                        _UpdatedChangeHistory.Add(changeHistory);
                    }
                    int history = await _reservoirService.InsertSafetyMeasureChangeHistory(_UpdatedChangeHistory);
                    result = await _reservoirService.InsertSafetyMeasuresFromExtract(safetyMeasure, _comment);

                }

            }

        }


        public static SafetyMeasuresChangeHistory AddHistory(int MeasureId,string OldValue, String NewValue, string FieldName, ReservoirSubmissionDTO submissionDetails)
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
                    changeHistory.ChangeDateTime = DateTime.Now;
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
                changeHistory.ChangeDateTime = DateTime.Now;
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

        //public async Task<int> CompareMIOSListWithDB(List<SafetyMeasure> safetyMeasures)
        //{
        //    int result = 0;
        //    bool compare;
        //   // var extractedcount;
            
        //    if (safetyMeasures == null) return 0;
        //    else
        //    {
        //        var uniqueReservoirid = safetyMeasures.Select(s => s.ReservoirId).Distinct();
        //        foreach(int reservoirid in uniqueReservoirid)
        //        {
        //            var _extractedSafetyMeasure = safetyMeasures.Where(s => s.ReservoirId == reservoirid);
        //            List<SafetyMeasure> _existingsafetyMeasure = await _reservoirService.GetSafetyMeasuresByReservoir(reservoirid);
        //            if (_existingsafetyMeasure == null)
        //                result = await _reservoirService.InsertSafetyMeasuresFromExtract(_extractedSafetyMeasure.First(), new Comment());
        //            else
        //            {
        //               var extractedcount = safetyMeasures.Where(s => s.ReservoirId == reservoirid).ToList();
        //                //int count = extractedcount.Count + 1;
        //                if ((extractedcount.Count == _existingsafetyMeasure.Count))
        //                    compare = CompareList(extractedcount, _existingsafetyMeasure);
        //                else
        //                    compare = CompareList(extractedcount, _existingsafetyMeasure);




        //            }
        //        }

        //    }


        //    return 1;
        //}

        //public static bool Compare(object e1, object e2)
        //{
        //    bool flag = true;
        //    foreach (PropertyInfo propObj1 in e1.GetType().GetProperties())
        //    {
        //        var propObj2 = e2.GetType().GetProperty(propObj1.Name);
        //        if (propObj1.PropertyType.Name.Equals("List`1"))
        //        {
        //            List<dynamic> objList1 = new List<object>() {
        //            propObj1.GetValue(e1, null)
        //        };
        //            List<dynamic> objList2 = new List<object>() {
        //            propObj2.GetValue(e2, null)
        //        };
        //            if (!CompareList(objList1[0], objList2[0]))
        //            {
        //                return false;
        //            }
        //        }
        //        else if (!(propObj1.GetValue(e1, null).Equals(propObj2.GetValue(e2, null))))
        //        {
        //            flag = false;
        //            return flag;
        //        }
        //    }
        //    return flag;
        //}

        //public static bool CompareList(dynamic object1, dynamic object2)
        //{
        //    bool match = false;
        //    List<dynamic> e1 = new List<dynamic>();
        //    e1.AddRange(object1);
        //    List<dynamic> e2 = new List<dynamic>();
        //    e2.AddRange(object2);
        //    int countFirst, countSecond;
        //    countFirst = e1.Count;
        //    countSecond = e2.Count;
        //    if (countFirst == countSecond)
        //    {
        //        countFirst = e1.Count - 1;
        //        while (countFirst > -1)
        //        {
        //            match = false;
        //            countSecond = e2.Count - 1;
        //            while (countSecond > -1)
        //            {
        //                match = Compare(e1[countFirst], e2[countSecond]);
        //                if (match)
        //                {
        //                    e2.Remove(e2[countSecond]);
        //                    countSecond = -1;
        //                }
        //                if (match == false && countSecond == 0)
        //                {
        //                    return false;
        //                }
        //                countSecond--;
        //            }
        //            countFirst--;
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    return match;
        //}
    }

   
}
