using RACE2.DataModel;
using RACE2.Dto;
using RACE2.Services;

namespace RACE2.BackEnd.Types.Mutation
{
    [MutationType]
    public class ReservoirMutationResolver
    {
        public async Task<Reservoir> UpdateReservoir(IReservoirService _reservoirService, ReservoirUpdateDetailsDTO updatedReservoir)
        {
            Reservoir existingReservoir= await _reservoirService.GetReservoirById(updatedReservoir.Id);    
            var result = await _reservoirService.UpdateReservoir(updatedReservoir);
            return result;
        }
    }
}
