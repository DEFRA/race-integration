using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class SendYourStatement
    {
        [Inject]
        public IConfiguration _config { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

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
            string pagelink = "/logout";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
        public void GoToMyAccountPage()
        {
            bool forceLoad = true;
            string pagelink = "/my-account";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
        public void GoToPrevPage()
        {
            bool forceLoad = true;
            string pagelink = "/upload-your-template";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
        public void GoToAnnualStatementsPage()
        {
            bool forceLoad = true;
            string pagelink = "/annual-statements";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
