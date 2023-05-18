using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace RACE2.FrontEndWeb.Pages.S12Pages
{
    public partial class Home
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        private IEnumerable<Claim>? Claims { get; set; }

        public string UserName { get; set; } = "Unknown";

        protected override async void OnInitialized()
        {
            //AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            //if (authState.User.Claims is not null)
            //{
            //    NavigationManager.NavigateTo("/annual-statements", true);
            //}
        }

        private async void GoToLoginPage()
        {
            bool forceLoad = true;
            NavigationManager.NavigateTo("/login", forceLoad);
        }

        private async void GoToNextPage()
        {
            bool forceLoad = true;
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            NavigationManager.NavigateTo("/annual-statements", forceLoad);
            //if (authState.User.Claims == null)
            //{
            //    Claims = authState.User.Claims;

            //    Claim? givenNameClaim = authState.User.FindFirst("name");
            //    if (givenNameClaim is not null)
            //    {
            //        UserName = givenNameClaim.Value;
            //    }
            //    NavigationManager.NavigateTo("/annual-statements", forceLoad);
            //}
            //else
            //{
            //    NavigationManager.NavigateTo("/login", forceLoad);
            //}
        }
    }
}
