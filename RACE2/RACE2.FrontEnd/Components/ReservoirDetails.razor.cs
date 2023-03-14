using Microsoft.AspNetCore.Components;
using RACE2.DataModel;

namespace RACE2.FrontEnd.Components
{
    public partial class ReservoirDetails
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

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
