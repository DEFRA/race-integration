using Microsoft.AspNetCore.Components;
using RACE2.Dto;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class NewReservoir
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

            public ReservoirDetailsDTO newReservoir = new ReservoirDetailsDTO();
 
        protected override void OnInitialized()
        {
              base.OnInitialized();
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/reservoir-not-listed";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
