using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.FrontEnd.RACE2GraphQLSchema;
using System.Security.Claims;

namespace RACE2.FrontEnd.Components
{
    public partial class AnnualStatements
    {
        [Inject]
        public RACE2GraphQLClient client { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        string CurrentUserEmail;
        bool? IsLoggedIn;
        private IEnumerable<Claim> UserClaims { get; set; }
        private string UserName { get; set; } = "Unknown";

        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            if (authState.User.Identity.Name is not null)
            {
                UserName = authState.User.Identity.Name;
                UserClaims = authState.User.Claims;
            }
        }

        public async void GoToNextPage()
        {
            bool forceLoad = false;
            string pagelink = "/choose-a-reservoir";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
