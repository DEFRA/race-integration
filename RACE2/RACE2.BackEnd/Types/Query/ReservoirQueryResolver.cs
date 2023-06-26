using RACE2.DataModel;
using RACE2.Dto;
using RACE2.Notification;
using RACE2.Services;

namespace RACE2.BackEnd.Types
{
    [QueryType]
    public class ReservoirQueryResolver
    {
        private readonly ILogger<ReservoirQueryResolver> _logger;

        public ReservoirQueryResolver(ILogger<ReservoirQueryResolver> logger)
        {
            _logger = logger;
        }

        public async Task<Reservoir> GetReservoirById(IReservoirService _reservoirService, int id)
        {
            var result = await _reservoirService.GetReservoirById(id);
            return result;
        }

        public async Task<List<ReservoirDetailsDTO>> GetReservoirsByUserId(IReservoirService _reservoirService, int id)
        {
            var result = await _reservoirService.GetReservoirsByUserId(id);
            return result;
        }

        //public async Task<List<ReservoirDetailsDTO>> GetReservoirsByUserEmailId(IReservoirService _reservoirService, string emailId)
        //{
        //    var result = await _reservoirService.GetReservoirsByUserEmailId(emailId);
        //    return result;
        //}

        public async Task<List<DataModel.Action>> GetActionsListByReservoirIdAndCategory(IReservoirService _reservoirService, int reservoirid, int category)
        {
            return await _reservoirService.GetActionsListByReservoirIdAndCategory(reservoirid, category);
        }

        public async Task<List<SafetyMeasure>> GetSafetyMeasuresListByReservoirId(IReservoirService _reservoirService, int reservoirid)
        {
            return await _reservoirService.GetSafetyMeasuresListByReservoirId(reservoirid);
        }

        public async Task<Address> GetAddressByReservoirId(IReservoirService _reservoirService, int reservoirid, string operatortype)
        {
            try
            {
                _logger.LogInformation("calling GetAddressByReservoirIdReselvor");
                return await _reservoirService.GetAddressByReservoirId(reservoirid, operatortype);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }

        }

        public async Task<List<OperatorDTO>> GetOperatorsforReservoir(IReservoirService _reservoirService, int reservoirid, string operatortype)
        {
            try
            {
                _logger.LogInformation("calling GetOperatorsforReservoir");
                return await _reservoirService.GetOperatorsforReservoir(reservoirid, operatortype);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
