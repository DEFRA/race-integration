﻿using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RACE2.Common;
using RACE2.DataModel;
using RACE2.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace RACE2.DataAccess.Repository
{
    public class ReservoirRepository : IReservoirRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly IConfiguration _configuration;
        public ReservoirRepository(IConfiguration configuration, ILogger<UserRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_configuration["SqlConnectionString"]);
            }
        }

        public async Task<Reservoir> GetReservoirById(int id)
        {
            using (var conn = Connection)
            {
                var query = @"Select *
                               from Reservoirs A                                   
                               Where A.Id=@Id";
                var reservoir = await conn.QuerySingleAsync<Reservoir>(query, new { Id = id });
                return reservoir;
            }
        }
        public async Task<Reservoir> UpdateReservoir(ReservoirUpdateDetailsDTO updatedReservoir)
        {
            try
            {
                using (var conn = Connection)
                {
                    int id = updatedReservoir.Id;
                    string updateQuery = @"UPDATE Reservoirs SET PublicName = @PublicName,GridReference=@GridReference,NearestTown=@NearestTown WHERE Id = @Id";
                    var parameters = new DynamicParameters();
                    parameters.Add("Id", id, DbType.Int32);
                    parameters.Add("PublicName", updatedReservoir.PublicName, DbType.String);
                    parameters.Add("GridReference", updatedReservoir.GridReference, DbType.String);
                    parameters.Add("NearestTown", updatedReservoir.NearestTown, DbType.String);
                    var reservoirUpdatedCount = await conn.ExecuteAsync(updateQuery, parameters);
                    if (reservoirUpdatedCount == 1)
                    {
                        var reservoirUpdated = await GetReservoirById(id);
                        return reservoirUpdated;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return null;
            };
        }

        ////public async Task<UserDetail> GetReservoirsByUserId(int id)
        //{
        //    using (var conn = Connection)
        //    {
        //        var query = @"Select A.Id, A.*,B.UserDetailId,B.ReservoirId,c.Id, c.*,d.id,d.AddressLine1,d.AddressLine2,d.town,d.postcode,d.county
        //                      from AspNetUsers A 
        //                      inner join UserReservoirs B ON  A.Id =b.UserDetailId 
        //                      inner join Reservoirs c On c.Id = b.ReservoirId 
        //                      inner join Addresses d On d.id = c.addressid
        //                      Where A.Id=@Id";

        //        var parameters = new DynamicParameters();
        //        parameters.Add("Id", id, DbType.Int32);
        //        var users = await conn.QueryAsync<UserDetail, UserReservoir, Address, UserDetail>(query, (user, reservoir, address) =>
        //        {
        //            reservoir.Reservoir.address = address;
        //            user.Reservoirs.Add(reservoir);

        //            return user;
        //        }, parameters, splitOn: "ReservoirId,id");
        //        var result = users.GroupBy(u => u.Id).Select(g =>
        //        {
        //            var groupedUser = g.First();
        //            groupedUser.Reservoirs = g.Select(u => u.Reservoirs.FirstOrDefault()).ToList();
        //            return groupedUser;
        //        });
        //        return result.FirstOrDefault();
        //    }
        //}

        public async Task<List<ReservoirDetailsDTO>> GetReservoirsByUserId(int id)
        {
            _logger.LogInformation("Getting Reservoir details for the user {id} ", id);

            try
            {
                if (id != 0)
                {
                    using (var conn = Connection)
                    {

                        var parameters = new DynamicParameters();
                        parameters.Add("id", id, DbType.String);
                        var reservoirs = await conn.QueryAsync<ReservoirDetailsDTO, UserDetail, ReservoirDetailsDTO>("sp_GetReservoirsbyUserId", (reservoir, userdetail) =>
                        {
                            // reservoir.Address = address;
                            reservoir.UserDetail = userdetail;

                            return reservoir;
                        }, parameters, null, true, splitOn: "ReservoirId", commandType: CommandType.StoredProcedure);
                        return reservoirs.ToList();
                    }
                }
                else
                {
                    _logger.LogInformation("THe input is not valid");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<DataModel.Action> GetActionsListByReservoirIdAndCategory(int reservoirid, int category, string reference)
        {
            var strCategory = (Category)category;
            using (var conn = Connection)
            {

                var parameters = new DynamicParameters();
                parameters.Add("reservoirid", reservoirid, DbType.Int64);
                parameters.Add("category", strCategory.ToString(), DbType.String);
                parameters.Add("reference", reference, DbType.String);
                var actionlist = await conn.QueryAsync<DataModel.Action>("sp_GetActionsListByReservoirId", parameters, commandType: CommandType.StoredProcedure);
                return actionlist.FirstOrDefault();
            }
        }

        //public async Task<List<SafetyMeasure>> GetSafetyMeasuresListByReservoirId(int reservoirid)
        //{
        //    using (var conn = Connection)
        //    {

        //        var parameters = new DynamicParameters();
        //        parameters.Add("reservoirid", reservoirid, DbType.Int64);
        //        var actionlist = await conn.QueryAsync<SafetyMeasure>("sp_GetSafetyMeasuresListByReservoirId", parameters, commandType: CommandType.StoredProcedure);
        //        return actionlist.ToList();
        //    }
        // }

        public async Task<Address> GetAddressByReservoirId(int reservoirid, string operatortype)
        {
            _logger.LogInformation("Getting Operator details of the reservoir {reservoirid} with operatorType as {operatortype}", reservoirid, operatortype);
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("reservoirid", reservoirid, DbType.Int64);
                    if (operatortype != null && operatortype == "Organisation")
                    {

                        var OrgAddress = await conn.QueryAsync<Address>("sp_GetAddressForReservoir", parameters, commandType: CommandType.StoredProcedure);

                        return OrgAddress.FirstOrDefault();
                    }
                    else if (operatortype != null && operatortype == "Individual")
                    {

                        var OrgAddress = await conn.QueryAsync<Address>("sp_GetAddressForReservoir", parameters, commandType: CommandType.StoredProcedure);

                        return OrgAddress.FirstOrDefault();
                    }
                    else
                    {
                        _logger.LogInformation($"The input is not valid {operatortype}");
                        return null;
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }



        }

        public async Task<List<OperatorDTO>> GetOperatorsforReservoir(int reservoirid, string operatortype)
        {
            _logger.LogInformation("Getting Operator details of the reservoir {reservoirid} with operatorType as {operatortype}", reservoirid, operatortype);
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("reservoirid", reservoirid, DbType.Int64);
                    if (operatortype != null && operatortype == "Organisation")
                    {

                        var operatorlist = await conn.QueryAsync<OperatorDTO>("sp_GetOperatorListAsOrgForReservoir", parameters, commandType: CommandType.StoredProcedure);

                        return operatorlist.ToList();
                    }
                    else if (operatortype != null && operatortype == "Individual")
                    {

                        var operatorlist = await conn.QueryAsync<OperatorDTO>("sp_GetOperatorListAsIndiviForReservoir", parameters, commandType: CommandType.StoredProcedure);

                        return operatorlist.ToList();
                    }
                    else
                    {
                        _logger.LogInformation($"The input is not valid {operatortype}");
                        return null;
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }


        }

        public async Task<List<SubmissionStatusDTO>> GetReservoirStatusByUserId(int id)
        {
            _logger.LogInformation("Getting Reservoir status for the user {id}", id);
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("id", id, DbType.String);
                    if (id != 0)
                    {

                        var operatorlist = await conn.QueryAsync<SubmissionStatusDTO>("sp_GetReservoirStatusByUserId", parameters, commandType: CommandType.StoredProcedure);

                        return operatorlist.ToList();
                    }

                    else
                    {
                        _logger.LogInformation($"The input is not valid or null {id}");
                        return null;
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }

        }

        public async Task<List<UndertakerDTO>> GetUndertakerforReservoir(int id)
        {
            _logger.LogInformation("Getting Undertaker details of the reservoirs for the assigned engineer {id} ", id);
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("id", id, DbType.Int64);
                    if (id != 0)
                    {

                        var operatorlist = await conn.QueryAsync<UndertakerDTO>("sp_GetUndertakerforReservoir", parameters, commandType: CommandType.StoredProcedure);

                        return operatorlist.ToList();
                    }
                    else
                    {
                        _logger.LogInformation($"The input is not valid {id}", id);
                        return null;
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }


        }


        public async Task<SubmissionStatus> UpdateReservoirStatus(int reservoirid, int userid, string reportStatus, bool IsRevision, string revisionSummary)
        {
            _logger.LogInformation("Updating reservoir status for the reservoir  {reservoirid} by the {userid}  ", reservoirid, userid);
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("reservoirid", reservoirid, DbType.Int64);
                    parameters.Add("userid", userid, DbType.Int64);
                    parameters.Add("reportStatus", reportStatus, DbType.String);
                    parameters.Add("isRevision", IsRevision, DbType.Boolean);
                    parameters.Add("revisionSummary", revisionSummary, DbType.String);
                    if (reservoirid != 0)
                    {

                        var operatorlist = await conn.QueryAsync<SubmissionStatus>("sp_UpdateReservoirStatus", parameters, commandType: CommandType.StoredProcedure);

                        return operatorlist.FirstOrDefault();
                    }
                    else
                    {
                        _logger.LogInformation("The input is not valid {reservoirid} by the {userid}  ", reservoirid, userid);
                        return null;
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }


        public async Task<int> InsertUploadDocumentDetails(DocumentDTO document)
        {
            int result = 0;
            _logger.LogInformation("Inserting uploaded document details for the reservoir");
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("filename", document.FileName, DbType.String);
                    parameters.Add("filelocation", document.FileLocation, DbType.String);
                    parameters.Add("filetype", document.FileType, DbType.String);
                    parameters.Add("documenttype", document.DocumentType, DbType.String);
                    parameters.Add("suppliedby", document.SuppliedBy, DbType.Int64);
                    parameters.Add("suppliedviaservice", document.SuppliedViaService, DbType.Int64);
                    parameters.Add("datesent", document.DateSent, DbType.DateTime);
                    parameters.Add("reservoirid", document.ReservoirId, DbType.Int64);
                    //parameters.Add("submissionid", document.SubmissionId, DbType.Int64);
                    //parameters.Add("documentName",document.DocumentName,DbType.String);
                    parameters.Add("blobStorageFileName", document.BlobStorageFileName, DbType.String);
                    parameters.Add("newid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                   
                        result = await conn.ExecuteAsync("sp_InsertDocumentUpload", parameters, commandType: CommandType.StoredProcedure);
                        var id = parameters.Get<int>("newid");
                        return id;

                       
                    


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 1;
            }
        }


        public async Task<int> UpdateScannedDocumentResult(DateTime scanneddatetime, bool isClean, string uploadblobpath, string documentName)
        {
            _logger.LogInformation("Updating scan result for the reservoir  {documentName}  ", documentName);
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("scannedtime", scanneddatetime, DbType.DateTime);
                    parameters.Add("isClean", isClean, DbType.Boolean);
                    parameters.Add("uploadBlobpath", uploadblobpath, DbType.String);
                    parameters.Add("documentName", documentName, DbType.String);
                    if (documentName != null)
                    {

                        await conn.ExecuteAsync("sp_UpdateScannedDocumentResult", parameters, commandType: CommandType.StoredProcedure);

                        return 1;
                    }
                    else
                    {
                        _logger.LogInformation("The input is not valid {documentName}", documentName);
                        return 0;
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 0;
            }
        }


        public async Task<DocumentDTO> GetScannedResultbyDocId(int id)
        {
            _logger.LogInformation("Get scan result for the document  {id}  ", id);
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("docid", id, DbType.Int64);

                    if (id != 0)
                    {

                        var result = await conn.QueryAsync<DocumentDTO>("sp_GetScannedResultByDocId", parameters, commandType: CommandType.StoredProcedure);

                        return result.FirstOrDefault();
                    }
                    else
                    {
                        _logger.LogInformation("The input is not valid {id}", id);
                        return null;
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<int> InsertDocumentRelatedTable(int reservoirid, int submissionid, int documentid)
        {
            _logger.LogInformation("Insert document related table for the document  {id}  ", documentid);
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("documentid", documentid, DbType.Int64);
                    parameters.Add("submissionid", submissionid, DbType.Int64);
                    parameters.Add("reservoirid", reservoirid, DbType.Int64);

                    if (documentid != 0)
                    {

                        var result = await conn.ExecuteAsync("sp_InsertDocumentRelatedTable", parameters, commandType: CommandType.StoredProcedure);

                        return 1;
                    }
                    else
                    {
                        _logger.LogInformation("The input is not valid {id}", documentid);
                        return 0;
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 0;
            }
        }


        public async Task<ReservoirSubmissionDTO> GetReservoirUserIdbySubRef(string submissionReference)
        {
            try
            {
                using (var conn = Connection)
                {

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("submissionreference", submissionReference, DbType.String);
                    var reservoir = await conn.QueryAsync<ReservoirSubmissionDTO>("sp_GetReservoirIdBySubmissionReference", parameters, commandType: CommandType.StoredProcedure);
                    return reservoir.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<int> InsertActionTableFromExtract(DataModel.Action action)
        {
            _logger.LogInformation("Insert Action table from Data extraction ");
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("reference", action.Reference, DbType.String);
                    parameters.Add("description", action.Description, DbType.String);
                    parameters.Add("mandatory", action.IsMandatory, DbType.Boolean);
                    parameters.Add("priority", action.Priority, DbType.String);
                    parameters.Add("reservoirid", action.ReservoirId, DbType.Int64);
                    var result = await conn.ExecuteAsync("sp_InsertActionFromExtract", parameters, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 0;
            }
            return 1;

        }

        public async Task<int> InsertorUpdateMaintenanceMeasureFromExtract(DataModel.Action action, Comment comment)
        {
            _logger.LogInformation("Insert Action and comment table from Data extraction ");
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("reference", action.Reference, DbType.String);
                    parameters.Add("description", action.Description, DbType.String);
                    parameters.Add("comment", comment.CommentText, DbType.String);
                    parameters.Add("isQualitycheck", comment.IsQualityCheckRequired, DbType.Boolean);
                    parameters.Add("userid", comment.CreatedByUserId, DbType.Int32);
                    parameters.Add("reservoirid", action.ReservoirId, DbType.Int32);
                    parameters.Add("relatestorecordid", comment.RelatesToRecordId, DbType.Int32);
                    parameters.Add("createddate", action.CreatedDate, DbType.DateTime);
                    parameters.Add("submissionid", comment.SourceSubmissionId, DbType.Int32);
                    var result = await conn.ExecuteAsync("sp_InsertMaintenanceMeasureFromExtract", parameters, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 0;
            }
            return 1;

        }

        public async Task<int> InsertorUpdateWatchItemsFromExtract(DataModel.Action action, Comment comment)
        {
            _logger.LogInformation("Insert Action watch items and comment table from Data extraction ");
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("reference", action.Reference, DbType.String);
                    parameters.Add("description", action.Description, DbType.String);
                    parameters.Add("comment", comment.CommentText, DbType.String);
                    parameters.Add("isQualitycheck", comment.IsQualityCheckRequired, DbType.Boolean);
                    parameters.Add("userid", comment.CreatedByUserId, DbType.Int32);
                    parameters.Add("reservoirid", action.ReservoirId, DbType.Int32);
                    parameters.Add("relatestorecordid", comment.RelatesToRecordId, DbType.Int32);
                    parameters.Add("createddate", action.CreatedDate, DbType.DateTime);
                    parameters.Add("submissionid", comment.SourceSubmissionId, DbType.Int32);
                    var result = await conn.ExecuteAsync("sp_InsertWatchItemsFromExtract", parameters, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 0;
            }
            return 1;
        }

        public async Task<int> InsertorUpdateSafetyMeasuresFromExtract(SafetyMeasure safetyMeasure, Comment comment)
        {
            _logger.LogInformation("Insert Safety Measure and comment table from Data extraction for {userid} ", comment.CreatedByUserId);
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("reference", safetyMeasure.Reference, DbType.String);
                    parameters.Add("description", safetyMeasure.Description, DbType.String);
                    parameters.Add("targetdate", safetyMeasure.TargetDate, DbType.DateTime);
                    parameters.Add("createddate", safetyMeasure.CreatedDate, DbType.DateTime);
                    parameters.Add("status", safetyMeasure.Status, DbType.String);
                    parameters.Add("comment", comment.CommentText, DbType.String);
                    parameters.Add("isqualitycheckrequired", comment.IsQualityCheckRequired, DbType.Boolean);
                    parameters.Add("userid", comment.CreatedByUserId, DbType.Int32);
                    parameters.Add("reservoirid", safetyMeasure.ReservoirId, DbType.Int32);
                    parameters.Add("relatestorecordid", comment.RelatesToRecordId, DbType.Int32);
                    parameters.Add("submissionid", comment.SourceSubmissionId, DbType.Int32);
                    var result = await conn.ExecuteAsync("sp_InsertSafetyMeasureFromExtract", parameters, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 0;
            }
            return 1;
        }

        public async Task<SafetyMeasure> GetSafetyMeasuresByReservoir(int reservoirid, string reference)
        {
            _logger.LogInformation("Getting safety Measure for the reservoir {id}", reservoirid);
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("reservoirid", reservoirid, DbType.Int32);
                    parameters.Add("reference", reference, DbType.String);
                    if (reservoirid != 0)
                    {

                        var safetymeasurelist = await conn.QueryAsync<SafetyMeasure>("sp_GetSafetyMeasureByReservoir", parameters, commandType: CommandType.StoredProcedure);

                        return safetymeasurelist.FirstOrDefault();
                    }

                    else
                    {
                        _logger.LogInformation($"The input is not valid or null {1}", reservoirid);
                        return null;
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<int> InsertSafetyMeasureChangeHistory(List<SafetyMeasuresChangeHistory> changeHistory)
        {

            _logger.LogInformation("Adding Safety Measure channge History");
            try
            {

                using (var conn = Connection)
                {
                    foreach (var change in changeHistory)
                    {

                        var parameters = new DynamicParameters();
                        parameters.Add("reservoirid", change.ReservoirId, DbType.Int32);
                        parameters.Add("submissionid", change.SourceSubmissionId, DbType.Int32);
                        parameters.Add("measureId", change.MeasureId, DbType.Int32);
                        parameters.Add("fieldName", change.FieldName, DbType.String);
                        parameters.Add("oldValue", change.OldValue, DbType.String);
                        parameters.Add("newValue", change.NewValue == null ? string.Empty : change.NewValue, DbType.String);
                        parameters.Add("changeDateTime", change.ChangeDateTime, DbType.DateTime2);
                        parameters.Add("isBackendChange", change.IsBackEndChange, DbType.Boolean);
                        parameters.Add("userId", change.ChangeByUserId, DbType.Int32);

                        var result = await conn.ExecuteAsync("sp_InsertSafetyMeasuresChangeHistory", parameters, commandType: CommandType.StoredProcedure);


                    }

                    return 1;

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 0;
            }
        }


        public async Task<int> InsertActionChangeHistory(List<ActionsChangeHistory> changeHistory)
        {

            _logger.LogInformation("Adding Action channge History");
            try
            {

                using (var conn = Connection)
                {
                    foreach (var change in changeHistory)
                    {

                        var parameters = new DynamicParameters();
                        parameters.Add("@reservoirid", change.ReservoirId, DbType.Int32);
                        parameters.Add("@submissionid", change.SourceSubmissionId, DbType.Int32);
                        parameters.Add("@actionid", change.ActionId, DbType.Int32);
                        parameters.Add("@fieldname", change.FieldName, DbType.String);
                        parameters.Add("@oldvalue", change.OldValue == null ? string.Empty : change.OldValue, DbType.String);
                        parameters.Add("@newvalue", change.NewValue == null ? string.Empty : change.NewValue, DbType.String);
                        parameters.Add("@changetime", change.ChangeDateTime, DbType.DateTime2);
                        parameters.Add("@Isbackendchange", change.IsBackEndChange, DbType.Boolean);
                        parameters.Add("@changeuserid", change.ChangeByUserId, DbType.Int32);

                        var result = await conn.ExecuteAsync("sp_InsertActionsChangeHistory", parameters, commandType: CommandType.StoredProcedure);


                    }

                    return 1;

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 0;
            }
        }

        public async Task<int> UpdateReservoirDetailsFromExtract(Reservoir reservoir)
        {
            _logger.LogInformation("Updating Action channge History");
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@reservoirid", reservoir.Id);
                    parameters.Add("@reservoirName", reservoir.RegisteredName);
                    parameters.Add("@gridreference", reservoir.GridReference);
                    parameters.Add("@nearesttown", reservoir.NearestTown);
                    parameters.Add("@lastinspectiondate", reservoir.LastInspectionDate);
                    parameters.Add("@lastcertificationdate", reservoir.LastCertificationDate);
                    parameters.Add("@nextinspectiondate102", reservoir.NextInspectionDate102);
                    var result = await conn.ExecuteAsync("sp_UpdateReservoirDetailsFromExtract", parameters, commandType: CommandType.StoredProcedure);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 0;
            }
        }

        public async Task<int> InsertStatementDetailsFromExtract(StatementDetails statementDetails)
        {
            _logger.LogInformation("Insert statement details from extract");
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();

                    parameters.Add("@documentid", statementDetails.DocumentId);
                    parameters.Add("@statementType", statementDetails.StatementType);
                    parameters.Add("@periodStartdate", statementDetails.PeriodStartDate == null ? DBNull.Value : statementDetails.PeriodStartDate);
                    parameters.Add("@periodenddate", statementDetails.PeriodEndDate == null ? DBNull.Value : statementDetails.PeriodEndDate);
                    parameters.Add("@statementdate", statementDetails.StatementDate == null ? DBNull.Value : statementDetails.StatementDate);
                    var result = await conn.ExecuteAsync("sp_InsertStatementDetails", parameters, commandType: CommandType.StoredProcedure);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 0;
            }
        }

        public async Task<int> InsertReservoirDetailsChangeHistory(List<ReservoirDetailsChangeHistory> changeHistory)
        {
            _logger.LogInformation("Insert Reservoir change History");
            try
            {

                using (var conn = Connection)
                {
                    foreach (var change in changeHistory)
                    {

                        var parameters = new DynamicParameters();
                        parameters.Add("@reservoirid", change.ReservoirId, DbType.Int32);
                        parameters.Add("@submissionid", change.SourceSubmissionId, DbType.Int32);
                        parameters.Add("@fieldName", change.FieldName, DbType.String);
                        parameters.Add("@oldValue", change.OldValue == null ? string.Empty : change.OldValue, DbType.String);
                        parameters.Add("@newValue", change.NewValue == null ? string.Empty : change.NewValue, DbType.String);
                        parameters.Add("@changeDateTime", change.ChangeDateTime, DbType.DateTime2);
                        parameters.Add("@isBackendChange", change.IsBackEndChange, DbType.Boolean);
                        parameters.Add("@userId", change.ChangeByUserId, DbType.Int32);

                        var result = await conn.ExecuteAsync("sp_InsertReservoirsChangeDetailsHistory", parameters, commandType: CommandType.StoredProcedure);


                    }

                    return 1;

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 0;
            }
        }

        public async Task<int> GetDocumentId(string documentName)
        {
            _logger.LogInformation("Get document id ");
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("blobStorageName", documentName, DbType.String);

                    if (documentName != null)
                    {
                        var result = await conn.QueryAsync<int>("sp_GetDocumentId", parameters, commandType: CommandType.StoredProcedure);
                        return result.FirstOrDefault();
                    }
                    else
                    {
                        _logger.LogInformation("The input is not valid ");
                        return 0;
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 0;
            }
        }

        public async Task<int> InsertCommentChangeHistory(List<CommentsChangeHistory> changeHistory)
        {

            _logger.LogInformation("Adding comment channge History");
            try
            {

                using (var conn = Connection)
                {
                    foreach (var change in changeHistory)
                    {

                        var parameters = new DynamicParameters();
                        parameters.Add("@reservoirid", change.ReservoirId, DbType.Int32);
                        parameters.Add("@submissionid", change.SourceSubmissionId, DbType.Int32);
                        parameters.Add("@commentid", change.CommentId, DbType.Int32);
                        parameters.Add("@fieldname", change.FieldName, DbType.String);
                        parameters.Add("@oldvalue", change.OldValue == null ? string.Empty : change.OldValue, DbType.String);
                        parameters.Add("@newvalue", change.NewValue == null ? string.Empty : change.NewValue, DbType.String);
                        parameters.Add("@changetime", change.ChangeDateTime, DbType.DateTime2);
                        parameters.Add("@Isbackendchange", change.IsBackEndChange, DbType.Boolean);
                        parameters.Add("@changeuserid", change.ChangeByUserId, DbType.Int32);

                        var result = await conn.ExecuteAsync("sp_InsertCommentsChangeHistory", parameters, commandType: CommandType.StoredProcedure);


                    }

                    return 1;

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 0;
            }
        }


        public async Task<Comment> GetExisitngComments(string relatestoobject, int relatestorecordid)
        {
            _logger.LogInformation("Get comments");
            try
            {

                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("relatestoobject", relatestoobject, DbType.String);
                    parameters.Add("relatestorecordid", relatestorecordid, DbType.Int32);
                    var result = await conn.QueryAsync<Comment>("sp_GetComments", parameters, commandType: CommandType.StoredProcedure);
                    return result.FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<SubmissionStatus> InsertSubmissionDetails(SubmissionStatus submissionStatus)
        {
            _logger.LogInformation("Insert new Submission for reservoir" + submissionStatus.ReservoirId );
            try
            {
                using (var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@reservoirid", submissionStatus.ReservoirId, DbType.Int32);
                    parameters.Add("@submissionreference", submissionStatus.SubmissionReference, DbType.String);
                    parameters.Add("@lastmodifiedtime", submissionStatus.LastModifiedDateTime, DbType.DateTime);                   
                    parameters.Add("@userid", submissionStatus.SubmittedByUserId, DbType.Int32);
                    parameters.Add("@isRevision", submissionStatus.IsRevision, DbType.Boolean);
                    parameters.Add("@revisionSummary", submissionStatus.RevisionSummary, DbType.String);
                    parameters.Add("@serviceid", submissionStatus.ServiceId, DbType.Int32);
                    parameters.Add("@submitteddatetime", submissionStatus.SubmittedDateTime, DbType.DateTime);
                    parameters.Add("@status", submissionStatus.Status, DbType.String);

                    var result = await conn.QueryAsync<SubmissionStatus>("sp_InsertSubmissionRecord", parameters, commandType: CommandType.StoredProcedure);

                    return result.FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
            
        }

        public string GenerateSubmissionReference(int reservoirid, DateTime submitteddatetime,int serviceid)
        {
            string submissionreference = "100000";
            _logger.LogInformation("Generate Submission Reference");
            try
            {
                if (reservoirid != 0)
                {
                    int length = Convert.ToString(reservoirid).Length;
                    submissionreference = submissionreference.Remove(submissionreference.Length - length) + Convert.ToString(reservoirid);

                    submissionreference = String.Concat(submissionreference, serviceid.ToString("00"), "_", submitteddatetime.Year, submitteddatetime.Month, submitteddatetime.Date.ToString("dd"), "_", submitteddatetime.Hour, submitteddatetime.Minute, submitteddatetime.Second);

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }


            return submissionreference;
        }

        public async Task<DateTime> GetLastSubmittedDateforReservoir(int reservoirid)
        {
            DateTime lastsent = new DateTime();
            _logger.LogInformation("GEt Last submitted date" + reservoirid);
            try
            {
                using (var conn = Connection)
                {
                    if (reservoirid != 0)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@reservoirid", reservoirid, DbType.Int32);
                        parameters.Add("@submittedtime", dbType: DbType.DateTime, direction: ParameterDirection.Output);
                        await conn.ExecuteAsync("sp_GetLastSubmittedDateforReservoir", parameters, commandType: CommandType.StoredProcedure);

                        lastsent = parameters.Get<DateTime>("@submittedtime");
                        return lastsent;

                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new DateTime();
            }
           return lastsent;
        }


        public async Task<DocumentTemplate> GetDocumentTemplate(int reservoirid)
        {
            try
            {
                using(var conn = Connection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@reservoirid", reservoirid, DbType.Int32);
                    var result = await conn.QueryAsync<DocumentTemplate>("sp_GetTemplateName", parameters, commandType: CommandType.StoredProcedure);
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

    }
}