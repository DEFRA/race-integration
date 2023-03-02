using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.FrontEndWeb.FluxorImplementation.Actions;
using RACE2.FrontEndWeb.FluxorImplementation.Stores;
using RACE2.FrontEndWeb.RACE2GraphQLSchema;
using RACE2.FrontEndWeb.Utilities;

namespace RACE2.FrontEndWeb.Components
{
    public partial class ChangePassword
    {
        [Inject]
        public RACE2GraphQLClient client { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IPasswordHasher<UserDetail> passwordHasher { get; set; } = default!;

        [Inject]
        public IState<AppStore> State { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        public AppStore AppStore => State.Value;

        string passwordRegEx = @"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$";
        private UserDetail CurrentUser;

        private EnterPasswordClass _enterPasswordClass = new EnterPasswordClass();
        protected override void OnInitialized()
        {
            if (AppStore.CurrentUserDetail is not null)
            {
                CurrentUser = AppStore.CurrentUserDetail;
            }
            if (AppStore.LastPasswordEntered is not null)
            {
                _enterPasswordClass.Password = _enterPasswordClass.ConfirmPassword = AppStore.LastPasswordEntered;
            }
            base.OnInitialized();
        }

        public async Task GoToNextPage()
        {
            string pw = _enterPasswordClass.Password;
            string cpw = _enterPasswordClass.ConfirmPassword;
            if (pw == cpw)
            {
                if (System.Text.RegularExpressions.Regex.Match(pw, passwordRegEx).Success)
                {
                    CurrentUser.PasswordHash = passwordHasher.HashPassword(CurrentUser, _enterPasswordClass.Password);
                    var action = new StoreUserDetailAction(CurrentUser);
                    Dispatcher.Dispatch(action);
                    var u=AppStore.CurrentUserDetail;
                    var action1 = new StoreLastPasswordEnteredAction(_enterPasswordClass.Password);
                    Dispatcher.Dispatch(action1);
                    var result = await client.UpdatePasswordHashForUser.ExecuteAsync(CurrentUser.Id, CurrentUser.PasswordHash);
                    var result1 = await client.MatchUserWithEmailAndPasswordHash.ExecuteAsync(CurrentUser.Email, CurrentUser.PasswordHash);
                    var action2 = new StoreIsLoggedInAction(true);
                    Dispatcher.Dispatch(action2);
                    bool forceLoad = true;
                    NavigationManager.NavigateTo("/account-confirmation", forceLoad);
                }
                else
                {
                    _enterPasswordClass.ConfirmPassword = "Password is not strong enough!!!";
                }
            }
            else
            {
                _enterPasswordClass.ConfirmPassword = "Passwords do not match!!!";
            }
        }
        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/enter-email";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }

    public class EnterPasswordClass
    {
        public string ExistingPassword = "";
        public string Password = "";
        public string ConfirmPassword = "";
    }
}
