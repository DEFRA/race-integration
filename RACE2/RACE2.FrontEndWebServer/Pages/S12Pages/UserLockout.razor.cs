using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class UserLockout
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        protected override async void OnInitialized()
        {
            await base.OnInitializedAsync();
        }        
    }
}
