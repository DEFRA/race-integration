using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.Dto;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class ReservoirNotListed
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        
        public ReservoirDetailsDTO newReservoir =new ReservoirDetailsDTO();

        protected override void OnInitialized()
        {
            //newReservoir = CurrentUserDetailState.NewReservoirDetails;
            base.OnInitialized();
        }

        bool forceLoad = false;
        public void GoToNextPage()
        {
            NavigationManager.NavigateTo("/new-reservoir", forceLoad);
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/choose-a-reservoir";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
