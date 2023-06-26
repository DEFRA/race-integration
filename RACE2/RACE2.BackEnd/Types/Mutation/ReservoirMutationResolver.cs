using RACE2.DataModel;
using RACE2.Dto;
using RACE2.Services;

namespace RACE2.BackEnd.Types.Mutation
{
    [MutationType]
    public class ReservoirMutationResolver
    {
        public async Task<Reservoir> UpdateReservoir(IReservoirService _reservoirService, int id,Reservoir updatedReservoir)
        {
            Reservoir existingReservoir= await _reservoirService.GetReservoirById(id);    
            var result = await _reservoirService.UpdateReservoir(existingReservoir.Id,updatedReservoir);
            return result;
        }
    }
}
