using Fluxor;
using Microsoft.AspNetCore.Components;
using RACE2.Dto;
using RACE2.FrontEndWebServer.FluxorImplementation.Stores;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class ConfirmPasswordChange
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        protected override async void OnInitialized()
        {
            //newReservoir = CurrentUserDetailState.ReservoirDetailsDTO;
            base.OnInitializedAsync();
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/reservoir-not-listed";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
