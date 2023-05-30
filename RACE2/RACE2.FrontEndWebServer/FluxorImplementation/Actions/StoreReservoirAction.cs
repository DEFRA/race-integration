using RACE2.DataModel;

namespace RACE2.FrontEndWebServer.FluxorImplementation.Actions
{
    public class StoreReservoirAction
    {
        public Reservoir CurrentReservoir { get; }
        public StoreReservoirAction(Reservoir reservoir)
            => CurrentReservoir = reservoir;
    }
}
