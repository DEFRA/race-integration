using RACE2.DataModel;
using RACE2.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Services
{
    public interface IReservoirService
    {
        Task<Reservoir> GetReservoirById(int id);
        Task<Reservoir> UpdateReservoir(ReservoirUpdateDetailsDTO updatedReservoir);
        //  public Task<UserDetail> GetReservoirsByUserId(int id);
        public Task<List<ReservoirDetailsDTO>> GetReservoirsByUserId(int id);
        public Task<List<ReservoirDetailsDTO>> GetReservoirsByUserEmailId(string emailId);

        public Task<DataModel.Action> GetActionsListByReservoirIdAndCategory(int reservoirid, int category, string reference);



        public Task<Address> GetAddressByReservoirId(int reservoirid, string operatortype);

        public Task<List<OperatorDTO>> GetOperatorsforReservoir(int reservoirid, string operatortype);

       // public Task<List<SubmissionStatusDTO>> GetReservoirStatusByUserId(int id);

        public Task<List<UndertakerDTO>> GetUndertakerforReservoir(int id);

       // public Task<SubmissionStatus> UpdateReservoirStatus(int reservoirid, int userid, string reportStatus, bool isRevision, string revisionSummary);

        public Task<int> InsertUploadDocumentDetails(DocumentDTO document);

        public Task<int> UpdateScannedDocumentResult(DateTime scanneddatetime, bool isClean, string uploadblobpath, string blobStorageFileName);

        public Task<DocumentDTO> GetScannedResultbyDocId(int id);

        public Task<int> InsertDocumentRelatedTable(int reservoirid, int submissionid, int documentid);

        public Task<ReservoirSubmissionDTO> GetReservoirUserIdbySubRef(string submissionReference);

        public Task<int> InsertActionTableFromExtract(DataModel.Action action);

        public Task<int> InsertorUpdateMaintenanceMeasureFromExtract(DataModel.Action action, Comment comment);

        public Task<int> InsertorUpdateWatchItemsFromExtract(DataModel.Action action, Comment comment);

        public Task<int> InsertorUpdateSafetyMeasuresFromExtract(SafetyMeasure safetyMeasure, Comment comment);

        public Task<SafetyMeasure> GetSafetyMeasuresByReservoir(int reservoirid, string reference);

        public Task<int> InsertSafetyMeasureChangeHistory(List<SafetyMeasuresChangeHistory> changeHistory);

        public Task<int> InsertActionChangeHistory(List<ActionsChangeHistory> changeHistory);

        public Task<int> UpdateReservoirDetailsFromExtract(Reservoir reservoir);

        public Task<int> InsertStatementDetailsFromExtract(StatementDetails statementDetails);

        public Task<int> InsertReservoirDetailsChangeHistory(List<ReservoirDetailsChangeHistory> changeHistory);
        public Task<int> GetDocumentId(string documentName);
        public Task<int> InsertCommentChangeHistory(List<CommentsChangeHistory> changeHistory);

        public Task<Comment> GetExisitngComments(string relatestoobject, int relatestorecordid);


        public string GenerateSubmissionReference(int reservoirid, DateTime submissiondate, int serviceid);

        public Task<int> InsertSubmissionDetails(SubmissionStatus submissionStatus);

        public Task<DateTime> GetLastSubmittedDateforReservoir(int reservoirid);

        public Task<DocumentTemplate> GetDocumentTemplate(int reservoirid);

    }
}