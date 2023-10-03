using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.FrontEndWebServer.FluxorImplementation.Stores;
using RACE2.Services;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class ConfirmChangePassword
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IUserService userService { get; set; } = default!;
        private string UserName { get; set; } = "Unknown";
        private UserDetail UserDetail { get; set; } = default!;

        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            UserName = authState.User.Claims.ToList().FirstOrDefault(c => c.Type == "name").Value;
            UserSpecificDto userDetails = await userService.GetUserByEmailID(UserName);
            UserDetail = new UserDetail()
            {
                UserName = UserName,
                Id = userDetails.Id,
                Email = userDetails.Email,
                c_first_name = userDetails.c_first_name,
                c_last_name = userDetails.c_last_name,
                c_IsFirstTimeUser = userDetails.c_IsFirstTimeUser
            };
            if (UserDetail.c_IsFirstTimeUser)
            {
                userService.UpdateFirstTimeUserLogin(userDetails.Email);
            }

            base.OnInitializedAsync();
        }
        public void GoToNextPage()
        {
            bool forceLoad = true;
            string pagelink = "/annual-statements";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/reservoir-not-listed";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
