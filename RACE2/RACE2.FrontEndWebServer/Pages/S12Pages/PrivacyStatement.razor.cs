﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Forms;
using System.IO;
using System.IO.Pipes;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Azure;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class PrivacyStatement
    {
        protected bool IsLoggedIn { get; set; } = false;
        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationStateTask { get; set; }
        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateTask; // AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (authState.User.Claims.Count() == 0)
                IsLoggedIn = false;
            else
                IsLoggedIn = true;
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
            await base.OnInitializedAsync();
        }
    }
}