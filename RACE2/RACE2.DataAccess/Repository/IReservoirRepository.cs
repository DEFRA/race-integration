using RACE2.DataModel;
using RACE2.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataAccess.Repository
{
    public interface IReservoirRepository
    {
        public Task<Reservoir> GetReservoirById(int id);
        public Task<Reservoir> UpdateReservoir(ReservoirUpdateDetailsDTO updatedReservoir);
        public Task<List<ReservoirDetailsDTO>> GetReservoirsByUserId(int id);
        
      
        public Task<Address> GetAddressByReservoirId(int reservoirid, string operatortype);

        public Task<List<OperatorDTO>> GetOperatorsforReservoir(int reservoirid, string operatortype);

        // public Task<List<>>

        public Task<List<SubmissionStatusDTO>> GetReservoirStatusByUserId(int id);

        public Task<List<UndertakerDTO>> GetUndertakerforReservoir(int id);

        public Task<SubmissionStatus> UpdateReservoirStatus(int reservoirid, int userid, string reportStatus);

        public Task<int> InsertUploadDocumentDetails(DocumentDTO document);

        public Task<int> UpdateScannedDocumentResult(DateTime scanneddatetime, bool isClean, string uploadblobpath, string documentName);

        public Task<DocumentDTO> GetScannedResultbyDocId(int id);

        public Task<int> InsertDocumentRelatedTable(int reservoirid, int submissionid, int documentid);
        public Task<ReservoirSubmissionDTO> GetReservoirUserIdbySubRef(string submissionReference);

        public Task<int> InsertActionTableFromExtract(DataModel.Action action);

        public Task<int> InsertorUpdateMaintenanceMeasureFromExtract(DataModel.Action action, Comment comment);

        public Task<int> InsertorUpdateWatchItemsFromExtract(DataModel.Action action, Comment comment);

        public Task<int> InsertorUpdateSafetyMeasuresFromExtract(SafetyMeasure safetyMeasure, Comment comment);

        public Task<SafetyMeasure> GetSafetyMeasuresByReservoir(int reservoirid, string reference);
        public Task<DataModel.Action> GetActionsListByReservoirIdAndCategory(int reservoirid, int category, string reference);

        public Task<int> InsertSafetyMeasureChangeHistory(List<SafetyMeasuresChangeHistory> changeHistory);
        
        public Task<int> InsertActionChangeHistory(List<ActionsChangeHistory> changeHistory);


    }
}
