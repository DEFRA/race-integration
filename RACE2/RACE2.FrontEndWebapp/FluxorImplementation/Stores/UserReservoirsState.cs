using Fluxor;
using RACE2.DataModel;

namespace RACE2.FrontEndWebapp.FluxorImplementation.Stores
{
    [FeatureState]
    public class UserReservoirsState
    {
        public List<Reservoir> UserReservoirs { get; }

        private UserReservoirsState() { } // Required for creating initial state

        public UserReservoirsState(List<Reservoir> userReservoirs)
        {
            UserReservoirs = userReservoirs;
        }
    }
}
