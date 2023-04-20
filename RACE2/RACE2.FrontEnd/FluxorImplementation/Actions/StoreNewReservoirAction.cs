using RACE2.DataModel;
using RACE2.Dto;

namespace RACE2.FrontEnd.FluxorImplementation.Actions
{
    public class StoreNewReservoirAction
    {
        public ReservoirDetailsDTO? NewReservoirDetails { get; }
        public StoreNewReservoirAction(ReservoirDetailsDTO? newReservoirDetails)
            => NewReservoirDetails = newReservoirDetails;
    }
}
