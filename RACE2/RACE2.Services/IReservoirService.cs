﻿using RACE2.DataModel;
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
  
        public Task<List<DataModel.Action>> GetActionsListByReservoirIdAndCategory(int reservoirid, int category);

        public Task<List<SafetyMeasure>> GetSafetyMeasuresListByReservoirId(int reservoirid);

        public Task<Address> GetAddressByReservoirId(int reservoirid, string operatortype);

        public Task<List<OperatorDTO>> GetOperatorsforReservoir(int reservoirid, string operatortype);

        public Task<List<SubmissionStatusDTO>> GetReservoirStatusByUserId(int id);

        public Task<List<UndertakerDTO>> GetUndertakerforReservoir(int id);

    }
}
