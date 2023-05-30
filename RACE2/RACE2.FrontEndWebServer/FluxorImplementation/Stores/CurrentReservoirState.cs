using Fluxor;
using RACE2.DataModel;

namespace RACE2.FrontEndWebServer.FluxorImplementation.Stores
{
    [FeatureState]
    public class CurrentReservoirState
    {
        public Reservoir CurrentReservoir { get; }

        private CurrentReservoirState() { } // Required for creating initial state

        public CurrentReservoirState(Reservoir currentReservoir)
        {
            CurrentReservoir = currentReservoir;
        }
    }
}
