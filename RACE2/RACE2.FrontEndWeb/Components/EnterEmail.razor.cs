using Fluxor;
using Microsoft.AspNetCore.Components;
using RACE2.DataModel;
using RACE2.FrontEndWeb.FluxorImplementation.Actions;
using RACE2.FrontEndWeb.FluxorImplementation.Stores;
using RACE2.FrontEndWeb.RACE2GraphQLSchema;
using RACE2.FrontEndWeb.Utilities;
using System;

namespace RACE2.FrontEndWeb.Components
{
    public partial class EnterEmail
    {
        [Inject]
        public RACE2GraphQLClient client { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IState<AppStore> State { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        public AppStore AppStore => State.Value;

        string? Email;
        private UserDetail CurrentUser;

        private string errorMsg1 = "Email entered does not exist!!! Please try again.";
        private string errorMsg2 = "Input is not a valid email address!!! Please try again.";

        protected override void OnInitialized()
        {
            if (AppStore.CurrentUserDetail != null)
            {
                if (AppStore.CurrentUserDetail is not null)
                {
                    CurrentUser = AppStore.CurrentUserDetail;
                    Email = CurrentUser.Email;
                }
            }
            base.OnInitialized();
        }

        protected async Task<string> GetEmailFromDatabase(string email)
        {
            var result = await client.GetUserByEmailID.ExecuteAsync(email);
            if (result != null && result.Data != null && result.Data.UserByEmailID.Email != null)
            {
                string pwh = result!.Data!.UserByEmailID.PasswordHash;
                CurrentUser = new UserDetail()
                {
                    Id = result!.Data!.UserByEmailID.Id,
                    Email = result!.Data!.UserByEmailID.Email,
                    UserName = result!.Data!.UserByEmailID.UserName,
                    PasswordHash = result!.Data!.UserByEmailID.PasswordHash
                };
                return result!.Data!.UserByEmailID.Email;
            }
            else
                return "";
        }

        public async Task GoToNextPage()
        {
            if (Email != null && Email.Trim().Length != 0 && RegexUtilities.IsValidEmail(Email))
            {
                bool forceLoad = false;

                string emailInDatabase = await GetEmailFromDatabase(Email);
                if (emailInDatabase != null && emailInDatabase.Trim().Length != 0)
                {
                    var action = new StoreUserDetailAction(CurrentUser);                   
                    Dispatcher.Dispatch(action);
                    if (CurrentUser.PasswordHash is null)
                    {
                        string pagelink = "/enter-password";
                        NavigationManager.NavigateTo(pagelink, forceLoad);
                    }
                    else
                    {
                        string pagelink = "/change-password";
                        NavigationManager.NavigateTo(pagelink, forceLoad);
                    }
                }
                else
                {
                    Email = errorMsg1;                   
                }
            }
            else
            {
                Email = errorMsg2;                
            }
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
