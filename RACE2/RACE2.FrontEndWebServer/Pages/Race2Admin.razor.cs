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
        public void GoToNextPage()
        {
            NavigationManager.NavigateTo("/new-reservoir", forceLoad);
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/choose-a-reservoir";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
