using RACE2.DataModel;
using RACE2.Dto;
using RACE2.Services;

namespace RACE2.WebApi.Types.Mutation
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

        public async Task<SubmissionStatus> UpdateReservoirStatus(IReservoirService _reservoirService, int reservoirid, int userid, string reportStatus)
        {
            try
            {
              //  _logger.LogInformation("calling UpdateReservoirStatus");
                return await _reservoirService.UpdateReservoirStatus(reservoirid, userid, reportStatus);
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }



}
