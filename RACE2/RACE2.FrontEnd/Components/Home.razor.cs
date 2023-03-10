using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace RACE2.FrontEnd.Components
{
    public partial class Home
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        private IEnumerable<Claim> UserClaims { get; set; }
        private string UserName { get; set; } = "Unknown";

        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (authState is not null)
            {
                NavigationManager.NavigateTo("/choose-a-reservoir", true);
            }
        }
        public async void GoToNextPage()
        {
            bool forceLoad = true;
            //AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            NavigationManager.NavigateTo("/authentication/login", forceLoad);
            //if (authState is null)
            //{
            //    NavigationManager.NavigateTo("/authentication/login", forceLoad);
            //}
            //else
            //{
            //    UserName = authState.User.Identity.Name;
            //    UserClaims = authState.User.Claims;

            //    NavigationManager.NavigateTo("/choose-a-reservoir", forceLoad);
            //}            
        }
    }
}
