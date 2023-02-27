using Fluxor;
using RACE2.FrontEndWeb.FluxorImplementation.Stores;
using RACE2.DataModel;
using RACE2.Dto;

namespace RACE2.FrontEndWeb.FluxorImplementation.Features
{
    public class AppFeature : Feature<AppStore>
    {
        public override string GetName()
          => nameof(AppStore);

        protected override AppStore GetInitialState()
          => new AppStore(
               IsLoading: false,
               CurrentReservoir: new Reservoir(),
               NewReservoirDetails: new NewReservoirDetails(),
               CurrentUserDetail: new UserDetail()
            );
    }
}
