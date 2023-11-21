using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.Services;

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
        bool IsAdmin =false;
        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationStateTask { get; set; }
        protected async override Task OnInitializedAsync()
        {
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

        private void goToLogoutPage()
        {
            bool forceLoad = true;
            string pagelink = "/Logout";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
