using Fluxor;
using RACE2.FrontEndWeb.FluxorImplementation.Stores;

namespace RACE2.FrontEndWeb.FluxorImplementation.Features
{
    public class AppFeature : Feature<AppStore>
    {
        public override string GetName()
          => nameof(AppStore);

        protected override AppStore GetInitialState()
          => new AppStore(
               IsLoading: false,
               CurrentReservoir: new (),
               CurrentUserDetail: new DataModel.UserDetail()
            );
    }
}
