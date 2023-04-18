using RACE2.DataModel;
using RACE2.Dto;

namespace RACE2.FrontEndWeb.FluxorImplementation.Actions
{
    public class StoreNewReservoirAction
    {
        public ReservoirDetailsDTO? ReservoirDetailsDTO { get; }
        public StoreNewReservoirAction(ReservoirDetailsDTO? newReservoirDetails)
            => ReservoirDetailsDTO = newReservoirDetails;
    }
}
