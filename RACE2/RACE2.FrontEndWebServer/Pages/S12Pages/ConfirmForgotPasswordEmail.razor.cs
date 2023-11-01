﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RACE2.DataModel;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class ConfirmForgotPasswordEmail
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IConfiguration _config { get; set; } = default!;

        private UserDetail UserDetail { get; set; } = new UserDetail();
        protected override async void OnInitialized()
        {
            await base.OnInitializedAsync();
        }

        public void GoToNextPage()
        {
            bool forceLoad = true;
            string pagelink = "/logout";       
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
