using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.FrontEndWebServer.FluxorImplementation.Stores;
using RACE2.Services;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class ConfirmChangePassword
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IUserService userService { get; set; } = default!;
        private string UserName { get; set; } = "Unknown";
        private UserDetail UserDetail { get; set; } = default!;
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
        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/reservoir-not-listed";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
