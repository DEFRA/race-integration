﻿using Fluxor;
using Microsoft.AspNetCore.Components;
using RACE2.FrontEnd.FluxorImplementation.Stores;

namespace RACE2.FrontEnd.Components
{
    public partial class AccountConfirmation
    {
        [Inject]
        public IState<CurrentUserDetailState> State { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        public CurrentUserDetailState CurrentUserDetailState => State.Value;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        private void changepassword()
        {
            bool forceLoad = false;
            string pagelink = "/change-password";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
        public async Task GoToNextPage()
        {
            bool forceLoad = false;
            string pagelink = "/choose-a-reservoir";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/change-password";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
