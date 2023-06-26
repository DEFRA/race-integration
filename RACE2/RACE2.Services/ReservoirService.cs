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
            return await _reservoirRepository.GetReservoirById(id);
        }
        public async Task<Reservoir> UpdateReservoir(int id, Reservoir updatedReservoir)
        {
                return await _reservoirRepository.UpdateReservoir(id, updatedReservoir);
        }

        //public async Task<UserDetail> GetReservoirsByUserId(int id)
        //{
        //    return await _userRepository.GetReservoirsByUserId(id);
        //}

        public async Task<List<ReservoirDetailsDTO>> GetReservoirsByUserId(int Id)
        {
            return await _reservoirRepository.GetReservoirsByUserId(Id);
        }

        public async Task<List<ReservoirDetailsDTO>> GetReservoirsByUserEmailId(string emailId)
        {
            var id = _userRepository.GetUserByEmailID(emailId).Result.Id;
            return await _reservoirRepository.GetReservoirsByUserId(id);
        }

        public async Task<List<DataModel.Action>> GetActionsListByReservoirIdAndCategory(int reservoirid, int category)
        {
            return await _reservoirRepository.GetActionsListByReservoirIdAndCategory(reservoirid, category);
        }

        public async Task<List<SafetyMeasure>> GetSafetyMeasuresListByReservoirId(int reservoirid)
        {
            return await _reservoirRepository.GetSafetyMeasuresListByReservoirId(reservoirid);
        }

        public async Task<Address> GetAddressByReservoirId(int reservoirid, string operatortype)
        {
            return await _reservoirRepository.GetAddressByReservoirId(reservoirid, operatortype);
        }

        public async Task<List<OperatorDTO>> GetOperatorsforReservoir(int reservoirid, string operatortype)
        {
            return await _reservoirRepository.GetOperatorsforReservoir(reservoirid, operatortype);
        }

        public async Task<List<SubmissionStatusDTO>> GetReservoirStatusByEmail(string email)
        {
            return await _reservoirRepository.GetReservoirStatusByEmail(email);
        }
    }
}
