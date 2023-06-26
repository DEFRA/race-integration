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
        public Task<Reservoir> UpdateReservoir(int id, Reservoir reservoir);
        public Task<List<ReservoirDetailsDTO>> GetReservoirsByUserId(int id);
        public Task<List<DataModel.Action>> GetActionsListByReservoirIdAndCategory(int reservoirid, int category);
      
        public Task<List<SafetyMeasure>> GetSafetyMeasuresListByReservoirId(int reservoirid);
        //  public Task<UserDetail> GetReservoirsByUserEmailId(string email);

        public Task<Address> GetAddressByReservoirId(int reservoirid, string operatortype);

        public Task<List<OperatorDTO>> GetOperatorsforReservoir(int reservoirid, string operatortype);

        // public Task<List<>>

        public Task<List<SubmissionStatusDTO>> GetReservoirStatusByEmail(string email);
    }
}
