using Fluxor;
using RACE2.DataModel;

namespace RACE2.FrontEndApp.FluxorImplementation.Stores
{
    [FeatureState]
    public class NewReservoirState
    {
        public Reservoir NewReservoir { get; }

        private NewReservoirState() { } // Required for creating initial state

        public NewReservoirState(Reservoir newReservoir)
        {
            NewReservoir = newReservoir;
        }
    }
}
