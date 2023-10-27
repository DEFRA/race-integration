using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class ConfirmCreatePassword
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationStateTask { get; set; }
        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateTask;// AuthenticationStateProvider.GetAuthenticationStateAsync();
            await base.OnInitializedAsync();
        }

        public void GoToNextPage()
        {
            bool forceLoad = true;
            string pagelink = "/annual-statements";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
