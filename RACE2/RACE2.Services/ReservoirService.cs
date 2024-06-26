﻿using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2010.Word;
using RACE2.DataAccess.Repository;
using RACE2.DataModel;
using RACE2.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Services
{
    public class ReservoirService : IReservoirService
    {
        public IReservoirRepository _reservoirRepository;
        public IUserRepository _userRepository;
        public ReservoirService(IUserRepository userRepository, IReservoirRepository reservoirRepository)
        {
            _userRepository = userRepository;
            _reservoirRepository = reservoirRepository;
        }
        public async Task<Reservoir> GetReservoirById(int id)
        {
            try
            {
                return await _reservoirRepository.GetReservoirById(id);
            }
            catch (Exception ex)
            {
                return new Reservoir();
            }
        }
        public async Task<Reservoir> UpdateReservoir(ReservoirUpdateDetailsDTO updatedReservoir)
        {
            try
            {
                return await _reservoirRepository.UpdateReservoir(updatedReservoir);
            }
            catch (Exception ex)
            {
                return new Reservoir();
            }
        }

        //public async Task<UserDetail> GetReservoirsByUserId(int id)
        //{
        //    return await _userRepository.GetReservoirsByUserId(id);
        //}

        public async Task<List<ReservoirDetailsDTO>> GetReservoirsByUserId(int Id)
        {
            try
            {
                return await _reservoirRepository.GetReservoirsByUserId(Id);
            }
            catch (Exception ex)
            {
                return new List<ReservoirDetailsDTO>();
            }

        }

        public async Task<List<ReservoirDetailsDTO>> GetReservoirsByUserEmailId(string emailId)
        {
            try
            {
                var id = _userRepository.GetUserByEmailID(emailId).Result.Id;
                return await _reservoirRepository.GetReservoirsByUserId(id);
            }
            catch (Exception ex)
            {
                return new List<ReservoirDetailsDTO>();
            }

        }

        public async Task<DataModel.Action> GetActionsListByReservoirIdAndCategory(int reservoirid, int category, string reference)
        {
            try
            {
                return await _reservoirRepository.GetActionsListByReservoirIdAndCategory(reservoirid, category, reference);
            }
            catch (Exception ex)
            {
                return new DataModel.Action();
            }
        }

        //public async Task<List<SafetyMeasure>> GetSafetyMeasuresListByReservoirId(int reservoirid)
        //{
        //    try
        //    {
        //        return await _reservoirRepository.GetSafetyMeasuresListByReservoirId(reservoirid);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new List<SafetyMeasure>();
        //    }

        //}

        public async Task<Address> GetAddressByReservoirId(int reservoirid, string operatortype)
        {
            try
            {
                return await _reservoirRepository.GetAddressByReservoirId(reservoirid, operatortype);
            }
            catch (Exception ex)
            {
                return new Address();
            }

        }

        public async Task<List<OperatorDTO>> GetOperatorsforReservoir(int reservoirid, string operatortype)
        {
            try
            {
                return await _reservoirRepository.GetOperatorsforReservoir(reservoirid, operatortype);
            }
            catch (Exception ex)
            {
                return new List<OperatorDTO>();
            }

        }

        //public async Task<List<SubmissionStatusDTO>> GetReservoirStatusByUserId(int id)
        //{
        //    try
        //    {
        //        return await _reservoirRepository.GetReservoirStatusByUserId(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new List<SubmissionStatusDTO>();
        //    }

        //}

        public async Task<List<UndertakerDTO>> GetUndertakerforReservoir(int id)
        {
            try
            {
                return await _reservoirRepository.GetUndertakerforReservoir(id);
            }
            catch (Exception ex)
            {
                return new List<UndertakerDTO>();
            }

        }


        //public async Task<SubmissionStatus> UpdateReservoirStatus(int reservoirid, int userid, string reportStatus, bool isRevision, string revisionSummary)
        //{
        //    try
        //    {
        //        return await _reservoirRepository.UpdateReservoirStatus(reservoirid, userid, reportStatus, isRevision, revisionSummary);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new SubmissionStatus();
        //    }

        //}

        public async Task<int> InsertUploadDocumentDetails(DocumentDTO document)
        {
            try
            {
                return await _reservoirRepository.InsertUploadDocumentDetails(document);
            }
            catch (Exception ex)
            {
                return 1;
            }
        }



        public async Task<int> UpdateScannedDocumentResult(DateTime scanneddatetime, bool isClean, string uploadblobpath, string blobStorageFileName)
        {
            try
            {
                return await _reservoirRepository.UpdateScannedDocumentResult(scanneddatetime, isClean, uploadblobpath, blobStorageFileName);
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public async Task<DocumentDTO> GetScannedResultbyDocId(int id)
        {
            try
            {
                return await _reservoirRepository.GetScannedResultbyDocId(id);
            }
            catch (Exception ex)
            {
                return new DocumentDTO();
            }
        }

        public async Task<int> InsertDocumentRelatedTable(int reservoirid, int submissionid, int documentid)
        {
            try
            {
                return await _reservoirRepository.InsertDocumentRelatedTable(reservoirid, submissionid, documentid);
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public async Task<ReservoirSubmissionDTO> GetReservoirUserIdbySubRef(string submissionReference)
        {
            try
            {
                return await _reservoirRepository.GetReservoirUserIdbySubRef(submissionReference);
            }
            catch (Exception ex)
            {
                return new ReservoirSubmissionDTO();
            }
        }

        public async Task<int> InsertActionTableFromExtract(DataModel.Action action)
        {
            try
            {
                return await _reservoirRepository.InsertActionTableFromExtract(action);
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public async Task<int> InsertorUpdateMaintenanceMeasureFromExtract(DataModel.Action action, Comment comment)
        {
            try
            {
                return await _reservoirRepository.InsertorUpdateMaintenanceMeasureFromExtract(action, comment);
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public async Task<int> InsertorUpdateWatchItemsFromExtract(DataModel.Action action, Comment comment)
        {
            try
            {
                return await _reservoirRepository.InsertorUpdateWatchItemsFromExtract(action, comment);
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public async Task<int> InsertorUpdateSafetyMeasuresFromExtract(SafetyMeasure safetyMeasure, Comment comment)
        {
            try
            {
                return await _reservoirRepository.InsertorUpdateSafetyMeasuresFromExtract(safetyMeasure, comment);
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public async Task<SafetyMeasure> GetSafetyMeasuresByReservoir(int reservoirid, string reference)
        {
            try
            {
                return await _reservoirRepository.GetSafetyMeasuresByReservoir(reservoirid, reference);
            }
            catch (Exception ex)
            {
                return new SafetyMeasure();
            }
        }

        public async Task<int> InsertSafetyMeasureChangeHistory(List<SafetyMeasuresChangeHistory> changeHistory)
        {
            try
            {
                return await _reservoirRepository.InsertSafetyMeasureChangeHistory(changeHistory);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> InsertActionChangeHistory(List<ActionsChangeHistory> changeHistory)
        {
            try
            {
                return await _reservoirRepository.InsertActionChangeHistory(changeHistory);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> UpdateReservoirDetailsFromExtract(Reservoir reservoir)
        {
            try
            {
                return await _reservoirRepository.UpdateReservoirDetailsFromExtract(reservoir);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> InsertStatementDetailsFromExtract(StatementDetails statementDetails)
        {
            try
            {
                return await _reservoirRepository.InsertStatementDetailsFromExtract(statementDetails);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> InsertReservoirDetailsChangeHistory(List<ReservoirDetailsChangeHistory> changeHistory)
        {
            try
            {
                return await _reservoirRepository.InsertReservoirDetailsChangeHistory(changeHistory);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> GetDocumentId(string documentName)
        {
            try
            {
                return await _reservoirRepository.GetDocumentId(documentName);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> InsertCommentChangeHistory(List<CommentsChangeHistory> changeHistory)
        {
            try
            {
                return await _reservoirRepository.InsertCommentChangeHistory(changeHistory);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<Comment> GetExisitngComments(string relatestoobject, int relatestorecordid)
        {
            try
            {
                return await _reservoirRepository.GetExisitngComments(relatestoobject, relatestorecordid);
            }
            catch (Exception ex)
            {
                return new Comment();
            }
        }


        public string GenerateSubmissionReference(int reservoirid, DateTime duedate, int serviceid)
        {
            try
            {
                return _reservoirRepository.GenerateSubmissionReference(reservoirid,duedate,serviceid);
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        public async Task<SubmissionStatus> InsertSubmissionDetails(SubmissionStatus submissionStatus)
        {
            try
            {
                return await _reservoirRepository.InsertSubmissionDetails(submissionStatus);
            }
            catch (Exception ex)
            {
                return new SubmissionStatus() ;
            }
        }

        public async Task<DateTime> GetLastSubmittedDateforReservoir(int reservoirid)
        {
            try
            {
                return await _reservoirRepository.GetLastSubmittedDateforReservoir(reservoirid);
            }
            catch (Exception ex)
            {
                return new DateTime();
            }
        }

        public async Task<DocumentTemplate> GetDocumentTemplate(int reservoirid)
        {
            try
            {
                return await _reservoirRepository.GetDocumentTemplate(reservoirid);
            }
            catch (Exception ex)
            {
                return new DocumentTemplate();
            }
        }
    }
}