using RACE2.DataModel;
using RACE2.Dto;

namespace RACE2.FrontEnd.FluxorImplementation.Actions
{
    public class StoreNewReservoirAction
    {
        public NewReservoirDetails? NewReservoirDetails { get; }
        public StoreNewReservoirAction(NewReservoirDetails? newReservoirDetails)
            => NewReservoirDetails = newReservoirDetails;
    }
}
