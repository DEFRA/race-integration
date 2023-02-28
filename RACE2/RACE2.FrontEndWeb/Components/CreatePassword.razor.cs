using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.FrontEndWeb.FluxorImplementation.Actions;
using RACE2.FrontEndWeb.FluxorImplementation.Stores;
using RACE2.FrontEndWeb.RACE2GraphQLSchema;

namespace RACE2.FrontEndWeb.Components
{
    public partial class CreatePassword
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
        public UserDetail CurrentUser { get; set; } = new UserDetail();

        private EnterPasswordClass _enterPasswordClass = new EnterPasswordClass();
        protected override void OnInitialized()
        {
            CurrentUser = AppStore.CurrentUserDetail;
            _enterPasswordClass.Password = _enterPasswordClass.ConfirmPassword=AppStore.LastPasswordEntered;
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
                    var action1 = new StoreLastPasswordEnteredAction(_enterPasswordClass.Password);
                    Dispatcher.Dispatch(action1);
                    var result = await client.UpdatePasswordHashForUser.ExecuteAsync(CurrentUser.Id, CurrentUser.PasswordHash);
                    var result1 = await client.MatchUserWithEmailAndPasswordHash.ExecuteAsync(CurrentUser.Email, CurrentUser.PasswordHash);
                    var action2 = new StoreIsLoggedInAction(true);
                    Dispatcher.Dispatch(action2);
                    bool forceLoad = true;
                    NavigationManager.NavigateTo("/choose-a-reservoir/", forceLoad);
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
    }

    public class EnterPasswordClass
    {
        public string Password = "";
        public string ConfirmPassword = "";
    }
}
