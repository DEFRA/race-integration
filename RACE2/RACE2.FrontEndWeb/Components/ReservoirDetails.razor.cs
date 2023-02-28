using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.FrontEndWeb.FluxorImplementation.Stores;

namespace RACE2.FrontEndWeb.Components
{
    public partial class ReservoirDetails
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IState<AppStore> State { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        public AppStore AppStore => State.Value;
        public Reservoir CurrentReservoir { get; set; } = new Reservoir();
        public string ReservoirName { get; set; } = default!;

        protected override void OnInitialized()
        {
            CurrentReservoir = AppStore.CurrentReservoir;
        }
    }
}
