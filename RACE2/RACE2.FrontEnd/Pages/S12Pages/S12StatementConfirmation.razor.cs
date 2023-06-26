using Microsoft.AspNetCore.Components;

namespace RACE2.FrontEnd.Pages.S12Pages
{
    public partial class S12StatementConfirmation
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/annual-statements";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
