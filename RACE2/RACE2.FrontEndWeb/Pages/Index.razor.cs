using Microsoft.AspNetCore.Components;

namespace RACE2.FrontEndWeb.Pages
{
    public partial class Index
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        public void GoToNextPage()
        {
            bool forceLoad = true;
            NavigationManager.NavigateTo("/enter-email/", forceLoad);
        }
    }
}
