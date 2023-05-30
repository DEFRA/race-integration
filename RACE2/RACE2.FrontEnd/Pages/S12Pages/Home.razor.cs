﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace RACE2.FrontEnd.Pages.S12Pages
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
            if (authState.User.Identity.Name is not null)
            {
                bool forceLoad = false;
                NavigationManager.NavigateTo("/annual-statements", forceLoad);
            }
        }

        public async void GoToNextPage()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            bool forceLoad = false;
            if (authState.User.Identity.Name is not null)
            {
                NavigationManager.NavigateTo("/annual-statements", forceLoad);
            }
            else
            {
                NavigationManager.NavigateTo("/authentication/login", forceLoad);
            }

            //NavigationManager.NavigateTo("/authentication/login", forceLoad);
            //NavigationManager.NavigateTo("/annual-statements", forceLoad);
        }
    }
}
