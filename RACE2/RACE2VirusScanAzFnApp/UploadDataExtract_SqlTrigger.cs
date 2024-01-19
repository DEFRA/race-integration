using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RACE2.DataModel;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using RACE2.Services;
using RACE2.Dto;
using System.Globalization;

namespace RACE2VirusScanAzFnApp
{
    public class UploadDataExtract_SqlTrigger
    {
        private readonly ILogger<UploadDataExtract_SqlTrigger> _logger;
        private readonly IConfiguration _config;
        private readonly IReservoirService _reservoirService;

        public UploadDataExtract_SqlTrigger(ILogger<UploadDataExtract_SqlTrigger> logger, IConfiguration config, IReservoirService reservoirService)
        {
            _logger = logger;
            _config = config;
            _reservoirService = reservoirService;
        }

        // Visit https://aka.ms/sqltrigger to learn how to use this trigger binding
        [Function("UploadDataExtract_StatementDetailsTrigger")]
        public async Task Run(
            [SqlTrigger("RAW_StatementDetails", "SqlServerConnectionString")] IReadOnlyList<SqlChange<RAW_StatementDetails>> changes,
                FunctionContext context)
        {
            string connString = Environment.GetEnvironmentVariable("SqlServerConnectionString", EnvironmentVariableTarget.Process);
            ReservoirSubmissionDTO reservoirSubmission = new ReservoirSubmissionDTO();
           // BasicDetails basicDetails = new BasicDetails();
            ReservoirDetailsChangeHistory changeHistory = new ReservoirDetailsChangeHistory();
            List<ReservoirDetailsChangeHistory> reservoirDetailsChangeHistory = new List<ReservoirDetailsChangeHistory>();
            foreach (var change in changes)
            {
                _logger.LogInformation("SQL Changes: " + change.Operation);
                _logger.LogInformation("item inserteed" + System.Text.Json.JsonSerializer.Serialize(change.Item));
                string[] subs = change.Item.DocumentName.Split('_');
                reservoirSubmission = await _reservoirService.GetReservoirUserIdbySubRef(subs[0].ToString());
                //basicDetails = GetAllIds(change.Item.DocumentName, connString);
                //basicDetails.SubmissionId = submissionStatus.Id;
                //basicDetails.ReservoirId = submissionStatus.ReservoirId;
                //basicDetails.SubmittedUserId = submissionStatus.SubmittedByUser;
                Reservoir actualReservoir = GetReservoirValuesFromDB(reservoirSubmission.ReservoirId, connString);
                Reservoir UpdatedReservoir = CompareValues(connString, actualReservoir, (change.Item), reservoirSubmission);
                //    changeHistory  = AddHistory


                //UpdateDBAndChangeHistory(connString, actualReservoir,UpdatedReservoir, change.Item, basicDetails);
            }

        }




        //public static BasicDetails GetAllIds(string documentName, string connString)
        //{
        //    BasicDetails basicdetails = new BasicDetails();
        //    string[] subs = documentName.Split('_');

        //    var query = "SELECT * FROM SubmissionStatus where SubmissionReference = '" + subs[0].ToString() + "'";
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand(query, conn);
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            basicdetails.ReservoirId = Convert.ToInt16(reader["ReservoirId"]);
        //            basicdetails.SubmissionId = Convert.ToInt16(reader["Id"]);
        //            basicdetails.SubmittedUserId = Convert.ToInt16(reader["SubmittedByUserId"]);
        //            // ActualValues.LastCertificationDate = reader["LastCertificationDate"].ToString();
        //            //ActualValues.LastInspectionDate = reader["LastInspectionDate"].ToString();
        //            // ActualValues.NextInspectionDate = reader[""].ToString();
        //        }
        //        reader.Close();
        //    }
        //    return basicdetails;
        //}

        public static int GetDocumentId(string documentName, string connString)
        {
            int documentId = 0;
            StatementDetails statementDetails = new StatementDetails();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetDocumentId", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@blobStorageName", documentName));
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    documentId = Convert.ToInt32(reader["Id"]);
                }
                reader.Close();
            }
            return documentId;
        }
        public static ReservoirDetailsChangeHistory AddHistory(string OldValue, String NewValue, string FieldName, ReservoirSubmissionDTO submissionDetails)
        {
            ReservoirDetailsChangeHistory changeHistory = new ReservoirDetailsChangeHistory();
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


            }
            else
            {
                return null;
            }


            return changeHistory;


        }

        //public static RAW_StatementDetails GetActualValueFromDB(int reservoirid)
        //{
        //    string connString = "Server=tcp:pocracinfss1401.database.windows.net,1433;Initial Catalog=pocracinfdb1401;Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication = Active Directory Default;";
        //    RAW_StatementDetails ActualValues = new RAW_StatementDetails();
        //    var query = "SELECT * FROM Reservoirs where Id = " + reservoirid;
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand(query, conn);
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            ActualValues.ReservoirName = Convert.ToString(reader["RegisteredName"]);
        //            ActualValues.GridReference = reader["GridReference"].ToString();
        //            ActualValues.NearestTown = reader["NearestTown"].ToString();
        //            ActualValues.LastCertificationDate = reader["LastCertificationDate"].ToString();
        //            ActualValues.LastInspectionDate = reader["LastInspectionDate"].ToString();
        //            // ActualValues.NextInspectionDate = reader[""].ToString();
        //        }
        //        reader.Close();


        //        return ActualValues;


        //    }
        //}

        public static Reservoir GetReservoirValuesFromDB(int reservoirid, string connString)
        {
            Reservoir Actualreservoir = new Reservoir();
            var query = "SELECT * FROM Reservoirs where Id = " + reservoirid;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Actualreservoir.Id = Convert.ToInt32(reader["Id"]);
                        Actualreservoir.BackEndReservoirId = Convert.ToString(reader["BackEndReservoirId"]);
                        Actualreservoir.PublicName = Convert.ToString(reader["PublicName"]);
                        Actualreservoir.RegisteredName = Convert.ToString(reader["RegisteredName"]);
                        Actualreservoir.ReferenceNumber = Convert.ToString(reader["ReferenceNumber"]);
                        Actualreservoir.PublicCategory = Convert.ToString(reader["PublicCategory"]);
                        Actualreservoir.RegisteredCategory = Convert.ToString(reader["RegisteredCategory"]);
                        Actualreservoir.GridReference = Convert.ToString(reader["GridReference"]);
                        Actualreservoir.NearestTown = Convert.ToString(reader["NearestTown"]);
                        Actualreservoir.Capacity = Convert.ToInt32(reader["Capacity"]);
                        Actualreservoir.SurfaceArea = Convert.ToInt32(reader["SurfaceArea"]);
                        Actualreservoir.TopWaterLevel = Convert.ToDecimal(reader["TopWaterLevel"]);
                        Actualreservoir.HasMultipleDams = Convert.ToBoolean(reader["HasMultipleDams"]);
                        Actualreservoir.KeyFacts = Convert.ToString(reader["KeyFacts"]);
                        Actualreservoir.ConstructionStartDate = Convert.ToDateTime(reader["ConstructionStartDate"]);
                        Actualreservoir.VerifiedDetailsDate = Convert.ToDateTime(reader["VerifiedDetailsDate"]);
                        Actualreservoir.LastInspectionDate = Convert.ToDateTime(reader["LastInspectionDate"]);
                        // Actualreservoir.LastInspectionByUser = Convert.ToInt32(reader["LastInspectionByUserId"]);
                        Actualreservoir.LastInspectionEngineerName = Convert.ToString(reader["LastInspectionEngineerName"]);
                        Actualreservoir.LastInspectionEngineerPhone = Convert.ToString(reader["LastInspectionEngineerPhone"]);
                        Actualreservoir.LastCertificationDate = Convert.ToDateTime(reader["LastCertificationDate"]);
                        Actualreservoir.NextInspectionDate102 = Convert.ToDateTime(reader["NextInspectionDate102"]);
                        Actualreservoir.NextInspectionDate103 = Convert.ToDateTime(reader["NextInspectionDate103"]);
                        Actualreservoir.OperatorType = Convert.ToString(reader["OperatorType"]);


                    }
                    reader.Close();
                }
            }
            return Actualreservoir;
        }

        public static Reservoir CompareValues(string connString, Reservoir Actual, RAW_StatementDetails Updated, ReservoirSubmissionDTO submissionDetails)
        {
            //Dictionary<string, string> ObjDifference = new Dictionary<string, string>();
            List<ReservoirDetailsChangeHistory> reservoirDetailsChangeHistory = new List<ReservoirDetailsChangeHistory>();
            Reservoir updatedReservoir = new Reservoir();
            updatedReservoir = Actual;
            string UpdatedValue;
            string actualvalue;
            bool IsEarlyInspectionRequired;
            ReservoirDetailsChangeHistory changeHistory = null;
            StatementDetails statementDetails = new StatementDetails();
            statementDetails.DocumentId = GetDocumentId(Updated.DocumentName, connString);
            // if (ReferenceEquals(Actual, Updated)) return null;

            if ((Actual == null) || (Updated == null)) return null;
            // var result = true;
            foreach (var property in Updated.GetType().GetProperties())
            {
                switch (property.Name)
                {
                    // IsEarlyInspectionRequiredYes
                    // IsEarlyInspectionRequiredNo
                    case "ReservoirName":
                        
                        UpdatedValue = property.GetValue(Updated).ToString();
                        actualvalue = Actual.RegisteredName;
                        changeHistory = AddHistory(actualvalue, UpdatedValue, "RegisteredName", submissionDetails);
                        if (changeHistory != null)
                        {
                            updatedReservoir.RegisteredName = UpdatedValue;
                            changeHistory.Reservoir = updatedReservoir;
                            reservoirDetailsChangeHistory.Add(changeHistory);
                        }

                        break;
                    case "IsTypeOfStatement12_2":
                        if (Convert.ToString(property.GetValue(Updated)) == "Yes")
                        {
                            statementDetails.StatementType = "Section 12(2)";
                        }
                        else statementDetails.StatementType = "";
                        break;
                    case "IsTypeOfStatement12_2A":
                        if (Convert.ToString(property.GetValue(Updated)) == "Yes")
                        {
                            if (!string.IsNullOrEmpty(statementDetails.StatementType))
                                statementDetails.StatementType = statementDetails.StatementType + " & Section 12(2A)";
                            else statementDetails.StatementType = "Section 12(2A)";
                        }
                        break;
                    case "PeriodStart":
                        if (property.GetValue(Updated) == null)
                            statementDetails.PeriodStartDate = null;
                        else
                            statementDetails.PeriodStartDate = DateTime.ParseExact(property.GetValue(Updated).ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        break;
                    case "PeriodEnd":
                        if (property.GetValue(Updated) == null)
                            statementDetails.PeriodEndDate = null;
                        else
                            statementDetails.PeriodEndDate = DateTime.ParseExact(property.GetValue(Updated).ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture); //Convert.ToDateTime(property.GetValue(Updated));
                        break;
                    case "StatementDate":
                        if (property.GetValue(Updated) == null)
                            statementDetails.StatementDate = null;
                        else
                            statementDetails.StatementDate = DateTime.ParseExact(property.GetValue(Updated).ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        break;
                    case "NearestTown":
                        UpdatedValue = property.GetValue(Updated).ToString();
                        actualvalue = Actual.NearestTown;
                        changeHistory = AddHistory(actualvalue, UpdatedValue, "NearestTown", submissionDetails);
                        if (changeHistory != null)
                        {
                            updatedReservoir.NearestTown = UpdatedValue;
                            reservoirDetailsChangeHistory.Add(changeHistory);
                        }

                        break;
                    case "GridReference":
                        UpdatedValue = property.GetValue(Updated).ToString();
                        actualvalue = Actual.GridReference;
                        changeHistory = AddHistory(actualvalue, UpdatedValue, "GridReference", submissionDetails);
                        if (changeHistory != null)
                        {
                            updatedReservoir.GridReference = UpdatedValue;

                            reservoirDetailsChangeHistory.Add(changeHistory);
                        }

                        break;
                    case "LastInspectionDate":
                        UpdatedValue = Convert.ToString(property.GetValue(Updated));
                        actualvalue = Convert.ToString(Actual.LastInspectionDate);
                        changeHistory = AddHistory(actualvalue, UpdatedValue, "LastInspectionDate", submissionDetails);
                        if (changeHistory != null)
                        {
                            updatedReservoir.LastInspectionDate = DateTime.ParseExact(UpdatedValue, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            reservoirDetailsChangeHistory.Add(changeHistory);
                        }

                        break;
                    case "LastCertificationDate":
                        UpdatedValue = Convert.ToString(property.GetValue(Updated));
                        actualvalue = Convert.ToString(Actual.LastCertificationDate);
                        changeHistory = AddHistory(actualvalue, UpdatedValue, "LastCertificationDate", submissionDetails);
                        if (changeHistory != null)
                        {
                            updatedReservoir.LastCertificationDate = DateTime.ParseExact(UpdatedValue, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            reservoirDetailsChangeHistory.Add(changeHistory);
                        }

                        break;
                    case "IsEarlyInspectionRequiredYes":
                        if (Convert.ToString(property.GetValue(Updated)) == "Yes")
                            IsEarlyInspectionRequired = true;
                        break;
                    case "IsEarlyInspectionRequiredNo":
                        if (Convert.ToString(property.GetValue(Updated)) == "Yes")
                            IsEarlyInspectionRequired = false;
                        break;
                    case "NextInspectionDate":
                        UpdatedValue = Convert.ToString(property.GetValue(Updated));
                        actualvalue = Convert.ToString(Actual.NextInspectionDate102);
                        changeHistory = AddHistory(actualvalue, UpdatedValue, "NextInspectionDate102", submissionDetails);
                        if (changeHistory != null)
                        {
                            updatedReservoir.NextInspectionDate102 = DateTime.ParseExact(UpdatedValue, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            reservoirDetailsChangeHistory.Add(changeHistory);
                        }

                        break;

                    default:
                        break;
                }


            }
            // var query = "Update Reservoirs set  + reservoirid;
            var query = "INSERT INTO [dbo].[ReservoirDetailsChangeHistory]([ReservoirId],[SourceSubmissionId],[FieldName],[OldValue],[NewValue],[ChangeDateTime],[IsBackEndChange],[ChangeByUserId]) VALUES(@ReservoirId,@SourceSubmissionId,@FieldName,@OldValue,@NewValue,@ChangeDateTime,@IsBackEndChange,@ChangeByUserId)";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_UpdateReservoirDetailsFromExtract", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@reservoirid", updatedReservoir.Id));
                    cmd.Parameters.Add(new SqlParameter("@reservoirName", updatedReservoir.RegisteredName));
                    cmd.Parameters.Add(new SqlParameter("@gridreference", updatedReservoir.GridReference));
                    cmd.Parameters.Add(new SqlParameter("@nearesttown", updatedReservoir.NearestTown));
                    cmd.Parameters.Add(new SqlParameter("@lastinspectiondate", updatedReservoir.LastInspectionDate));
                    cmd.Parameters.Add(new SqlParameter("@lastcertificationdate", updatedReservoir.LastCertificationDate));
                    cmd.Parameters.Add(new SqlParameter("@nextinspectiondate102", updatedReservoir.NextInspectionDate102));
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand("sp_InsertStatementDetails", conn))
                {
                    //statementDetails.StatementType = "";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@documentid", statementDetails.DocumentId));
                    cmd.Parameters.Add(new SqlParameter("@statementType", statementDetails.StatementType));
                    cmd.Parameters.Add(new SqlParameter("@periodStartdate", statementDetails.PeriodStartDate == null ? DBNull.Value : statementDetails.PeriodStartDate));
                    cmd.Parameters.Add(new SqlParameter("@periodenddate", statementDetails.PeriodEndDate == null ? DBNull.Value : statementDetails.PeriodEndDate));
                    cmd.Parameters.Add(new SqlParameter("@statementdate", statementDetails.StatementDate == null ? DBNull.Value : statementDetails.StatementDate));
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    var rowsaffedted = conn.Execute(query, reservoirDetailsChangeHistory);
                }


            }
            return updatedReservoir;
        }

        //if (objValue == null && anotherValue == null)
        //{

        //}
        //else if ((objValue == null) || (anotherValue == null))
        //{
        //    var change = new ReservoirDetailsChangeHistory();
        //    change.FieldName = property.Name;
        //    change.OldValue = (objValue == null) ? null : objValue.ToString();
        //    change.NewValue = (anotherValue == null) ? null : anotherValue.ToString();
        //    reservoirDetailsChangeHistory.Add(change);
        //}

        //else if (!objValue.Equals(anotherValue) && objValue != null && anotherValue != null)
        //{
        //    var change = new ReservoirDetailsChangeHistory();
        //    change.FieldName = property.Name;
        //    change.OldValue = objValue.ToString();
        //    change.NewValue = anotherValue.ToString();

        //    reservoirDetailsChangeHistory.Add(change);
        //    result = false;
        //}
        //public static void UpdateDBAndChangeHistory(string connString, Reservoir actualValue, Reservoir updatedValue, RAW_StatementDetails changeItem ,BasicDetails basicDetails)
        //{
        //    string query;
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        // query = "select * from Reservoirs where";
        //        conn.Open();
        //        using (SqlCommand cmd = new SqlCommand("sp_UpdateReservoirDetailsFromExtract", conn))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            foreach (var change in reservoirDetailsChangeHistory)
        //            {
        //                List<PropertyInfo> properties = UpdatedValue.GetType().GetProperties().ToList();
        //                //  if (properties.Contains(change.FieldName))  //.Contains(change.FieldName))
        //                {

        //                    cmd.Parameters.Add(new SqlParameter("@reservoirid", basicDetails.ReservoirId));
        //                    //cmd.Parameters.Add(new SqlParameter("@reservoirName", isclean));
        //                    //cmd.Parameters.Add(new SqlParameter("@gridreference", blobpath));
        //                    //cmd.Parameters.Add(new SqlParameter("@nearesttown", documentName));
        //                    //cmd.Parameters.Add(new SqlParameter("@lastinspectiondate", scannedTime));
        //                    //cmd.Parameters.Add(new SqlParameter("@lastcertificationdate", isclean));
        //                    //cmd.Parameters.Add(new SqlParameter("@nextinspectiondate102", blobpath));
        //                    //cmd.Parameters.Add(new SqlParameter("@nextinspectiondate103", documentName));
        //                }
        //            }
        //            cmd.ExecuteNonQuery();
        //        }


        //    }
        //}


        public class BasicDetails
        {
            public int ReservoirId { get; set; }
            public int SubmissionId { get; set; }

            public int SubmittedUserId { get; set; }
        }

    }


}
