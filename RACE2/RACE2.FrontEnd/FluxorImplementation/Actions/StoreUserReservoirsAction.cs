using RACE2.DataModel;

namespace RACE2.FrontEnd.FluxorImplementation.Actions
{
    public class StoreUserReservoirsAction
    {
        public List<Reservoir> UserReservoirs { get; }
        public StoreUserReservoirsAction(List<Reservoir> userReservoirs)
            => UserReservoirs = userReservoirs;
    }
}
