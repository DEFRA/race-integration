using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class UploadConfirmation
    {
        [Inject]
        public IConfiguration _config { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Parameter]
        public string ReservoirId { get; set; }
        [Parameter]
        public string ReservoirRegName { get; set; }
        [Parameter]
        public string UndertakerName { get; set; }
        [Parameter]
        public string UndertakerEmail { get; set; }
        [Parameter]
        public string SubmissionReference { get; set; }
        [Parameter]
        public string YesNoValue { get; set; }
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
            string pagelink = $"/upload-your-template/{ReservoirId}/{ReservoirRegName}/{UndertakerName}/{UndertakerEmail}/{SubmissionReference}/{YesNoValue}";
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
