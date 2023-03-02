using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.FrontEndWeb.FluxorImplementation.Actions;
using RACE2.FrontEndWeb.FluxorImplementation.Stores;

namespace RACE2.FrontEndWeb.Components
{
    public partial class EnterPassword
    {
        [Inject]
        public IState<AppStore> State { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        public AppStore AppStore => State.Value;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IPasswordHasher<UserDetail> passwordHasher { get; set; } = default!;

        string? Password;
        string passwordRegEx = @"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$";
        UserDetail CurrentUser;
        protected override void OnInitialized()
        {
            if (AppStore.CurrentUserDetail is not null)
            {
                CurrentUser = AppStore.CurrentUserDetail;
            }
            base.OnInitialized();
        }
        private void changepassword()
        {
            bool forceLoad = false;
            string pagelink = "/change-password";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
        public async Task GoToNextPage()
        {
            if (System.Text.RegularExpressions.Regex.Match(Password, passwordRegEx).Success)
            {
                //SignInResult loginResult = await SignInManager!.CheckPasswordSignInAsync(CurrentUser, Password, false); 
                if (true)
                {
                    bool forceLoad = true;
                    NavigationManager.NavigateTo("/choose-a-reservoir", forceLoad);
                }
                else
                {
                    Password = "Password is correct. Try again.";
                    //change later
                    bool forceLoad = true;
                    NavigationManager.NavigateTo("/choose-a-reservoir", forceLoad);
                }
            }
            else
            {
                Password = "Password is not in correct format. Try again.";
            }
        }
        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/enter-email";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
