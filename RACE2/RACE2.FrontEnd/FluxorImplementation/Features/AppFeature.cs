using Fluxor;
using RACE2.FrontEnd.FluxorImplementation.Stores;
using RACE2.DataModel;
using RACE2.Dto;

namespace RACE2.FrontEnd.FluxorImplementation.Features
{
    public class AppFeature : Feature<AppStore>
    {
        public override string GetName()
          => nameof(AppStore);

        protected override AppStore GetInitialState()
          => new AppStore(
               IsLoading: false,
               IsLoggedIn: false,
               LastPasswordEntered:"",
               CurrentReservoir: new Reservoir(),
               NewReservoirDetails: new ReservoirDetailsDTO(),
               CurrentUserDetail: new UserDetail()
            );
    }
}
