using Microsoft.AspNetCore.Components;

namespace RACE2.FrontEnd.Components
{
    public partial class Home
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
