using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.FrontEnd.Features.CurrentUserDetail.Store;

namespace RACE2.FrontEnd.Components
{
    public partial class ReservoirDetails
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IState<CurrentUserDetailState> State { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        public CurrentUserDetailState CurrentUserDetailState => State.Value;

        public Reservoir CurrentReservoir { get; set; } = new Reservoir();
        public string ReservoirName { get; set; } = default!;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            //CurrentReservoir = CurrentUserDetailState.CurrentReservoir;
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/choose-a-reservoir";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
