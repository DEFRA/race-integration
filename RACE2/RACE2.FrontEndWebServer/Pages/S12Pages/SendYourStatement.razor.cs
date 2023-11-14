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
        [Parameter]
        public string ReservoirId { get; set; }
        [Parameter]
        public string ReservoirRegName { get; set; }
        [Parameter]
        public string UndertakerName { get; set; }
        [Parameter]
        public string UndertakerEmail { get; set; }
        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationStateTask { get; set; }
        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateTask;// AuthenticationStateProvider.GetAuthenticationStateAsync();
            await base.OnInitializedAsync();
        }
        private YesNoClass _yesno = new YesNoClass();

        public void GoToNextPage()
        {
            var YesNoValue = _yesno.YesNoOptions.ToString();

            bool forceLoad = false;

            NavigationManager.NavigateTo($"/upload-your-template/{ReservoirId}/{ReservoirRegName}/{UndertakerName}/{UndertakerEmail}/{YesNoValue}", forceLoad);
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
            string pagelink = "/annual-statements";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
        public void GoToAnnualStatementsPage()
        {
            bool forceLoad = true;
            string pagelink = "/annual-statements";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }

    public class YesNoClass
    {
        public enum YesNoEnum { Yes = 1, No = 2 };
        public YesNoEnum YesNoOptions = 0;
    }
}
