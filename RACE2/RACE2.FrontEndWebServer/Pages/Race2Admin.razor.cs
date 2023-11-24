using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.Services;
using System.ComponentModel.DataAnnotations;

namespace RACE2.FrontEndWebServer.Pages
{
    public partial class Race2Admin
    {
        [Inject]
        public IConfiguration _config { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IUserService userService { get; set; } = default!;
        private string UserName { get; set; } = "Unknown";
        private UserDetail UserDetail { get; set; } = default!;
        UserSpecificDto userDetails { get; set; }

        [Required]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$")]
        public string UserEmail { get; set; }

        private bool IsAdmin =false;
        private bool IsFirstTimeUser = false;
        private bool IsUserLockedOut = false;
        private string enabled = "hidden";
        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationStateTask { get; set; }
        protected async override Task OnInitializedAsync()
        {
            UserEmail = "kriss.sahoo@capgemini.com";
            enabled = "visible";
            var authState = await AuthenticationStateTask;
            UserName = authState.User.Claims.ToList().FirstOrDefault(c => c.Type == "name").Value;

            userDetails = await userService.GetUserWithRoles(UserName);
            foreach(var role in userDetails.roles)
            {
                if (role.Name == "System Administrator")
                {
                    IsAdmin = true;
                    break;
                }
            }
            base.OnInitializedAsync();
        }

        bool forceLoad = false;
        public void goToRegPage()
        {
            bool forceLoad = true;
            string pagelink = _config["RACE2SecurityProviderURL"] + "/Identity/Account/Register";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }

        private void goToChangePWPage()
        {
            bool forceLoad = true;
            string pagelink = _config["RACE2SecurityProviderURL"] + "/Identity/Account/ChangeExistingUserPassword";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
        private void OnValidSubmitFirstTimeUser()
        {
            IsUserLockedOut = false;
            IsFirstTimeUser = true;
            userService.UpdateFirstTimeUserLogin(UserEmail,true);
            IsFirstTimeUser = false;
            enabled = "visible";
            StateHasChanged();
        }

        private void OnValidSubmitUnlockUser()
        {
            IsUserLockedOut = true;
            IsFirstTimeUser = false;
            userService.ResetUserLockout(UserEmail);
            IsUserLockedOut = false;
            enabled = "visible";
            StateHasChanged();
        }

        private void goToLogoutPage()
        {
            bool forceLoad = true;
            string pagelink = "/Logout";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
