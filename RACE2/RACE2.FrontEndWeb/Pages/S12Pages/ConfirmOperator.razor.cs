using Microsoft.AspNetCore.Components;
using RACE2.FrontEndWeb.FluxorImplementation.Actions;

namespace RACE2.FrontEndWeb.Pages.S12Pages
{
    public partial class ConfirmOperator
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        public async void GoToNextPage()
        {

        }

        public async void GoToSaveComebackLaterPage()
        {

        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/confirm-reservoir";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
