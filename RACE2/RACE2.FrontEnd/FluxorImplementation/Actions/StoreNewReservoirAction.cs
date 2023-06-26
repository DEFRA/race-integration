using RACE2.DataModel;
using RACE2.Dto;

namespace RACE2.FrontEnd.FluxorImplementation.Actions
{
    public class StoreNewReservoirAction
    {
        public Reservoir NewReservoir { get; }
        public StoreNewReservoirAction(Reservoir newReservoir)
            => NewReservoir = newReservoir;
    }
}
