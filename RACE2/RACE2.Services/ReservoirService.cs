using DocumentFormat.OpenXml.Office2010.Excel;
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
    public class ReservoirService :IReservoirService
    {
        public IReservoirRepository _reservoirRepository;
        public IUserRepository _userRepository;
        public ReservoirService(IUserRepository userRepository,IReservoirRepository reservoirRepository)
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

        public async Task<List<DataModel.Action>> GetActionsListByReservoirIdAndCategory(int reservoirid, int category)
        {
            try
            {
                return await _reservoirRepository.GetActionsListByReservoirIdAndCategory(reservoirid, category);
            }
            catch (Exception ex)
            {
                return new List<DataModel.Action>();
            }
        }

        public async Task<List<SafetyMeasure>> GetSafetyMeasuresListByReservoirId(int reservoirid)
        {
            try
            {
                return await _reservoirRepository.GetSafetyMeasuresListByReservoirId(reservoirid);
            }
            catch (Exception ex)
            {
                return new List<SafetyMeasure>();
            }

        }

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

        public async Task<List<SubmissionStatusDTO>> GetReservoirStatusByUserId(int id)
        {
            try
            {
                return await _reservoirRepository.GetReservoirStatusByUserId(id);
            }
            catch (Exception ex)
            {
                return new List<SubmissionStatusDTO>();
            }
           
        }

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


        public async Task<SubmissionStatus> UpdateReservoirStatus(int reservoirid, int userid, string reportStatus)
        {
            try
            {
                return await _reservoirRepository.UpdateReservoirStatus(reservoirid, userid, reportStatus);
            }
            catch (Exception ex)
            {
                return new SubmissionStatus();
            }

        }
    }
}
