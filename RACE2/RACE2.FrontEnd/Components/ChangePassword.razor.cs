using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.FrontEnd.FluxorImplementation.Stores;
//using RACE2.FrontEnd.RACE2GraphQLSchema;
using RACE2.FrontEnd.Utilities;

namespace RACE2.FrontEnd.Components
{
    public partial class ChangePassword
    {
        //[Inject]
        //public RACE2GraphQLClient client { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IPasswordHasher<UserDetail> passwordHasher { get; set; } = default!;
        [Inject]
        public IState<CurrentUserDetailState> State { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        public CurrentUserDetailState CurrentUserDetailState => State.Value;

        string passwordRegEx = @"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$";
        private UserDetail CurrentUser;

        private EnterPasswordClass _enterPasswordClass = new EnterPasswordClass();
        protected override void OnInitialized()
        {
            if (CurrentUserDetailState.CurrentUserDetail is not null)
            {
                CurrentUser = CurrentUserDetailState.CurrentUserDetail;
            }
            //if (CurrentUserDetailState.LastPasswordEntered is not null)
            //{
            //    _enterPasswordClass.Password = _enterPasswordClass.ConfirmPassword = AppStore.LastPasswordEntered;
            //}
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
