using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.FrontEnd.Features.CurrentUserDetail.Store;

namespace RACE2.FrontEnd.Components
{
    public partial class EnterPassword
    {
        [Inject]
        public IState<CurrentUserDetailState> State { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        public CurrentUserDetailState CurrentUserDetailState => State.Value;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IPasswordHasher<UserDetail> passwordHasher { get; set; } = default!;

        string? Password;
        string passwordRegEx = @"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$";
        UserDetail CurrentUser;
        protected override void OnInitialized()
        {
            if (CurrentUserDetailState.CurrentUserDetail is not null)
            {
                CurrentUser = CurrentUserDetailState.CurrentUserDetail;
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
