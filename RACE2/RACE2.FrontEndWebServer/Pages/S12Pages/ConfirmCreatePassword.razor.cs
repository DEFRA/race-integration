using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.Services;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class ConfirmCreatePassword
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IUserService userService { get; set; } = default!;
        private string UserName { get; set; } = "Unknown";
        private UserDetail UserDetail { get; set; } = default!;
        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationStateTask { get; set; }
        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateTask;// AuthenticationStateProvider.GetAuthenticationStateAsync();
            UserName = authState.User.Claims.ToList().FirstOrDefault(c => c.Type == "name").Value;
            UserSpecificDto userDetails = await userService.GetUserByEmailID(UserName);
            UserDetail = new UserDetail()
            {
                UserName = UserName,
                Id = userDetails.Id,
                Email = userDetails.Email,
                cFirstName = userDetails.cFirstName,
                cLastName = userDetails.cLastName,
                cIsFirstTimeUser = userDetails.cIsFirstTimeUser
            };
            if ((bool)UserDetail.cIsFirstTimeUser)
            {
                await userService.UpdateFirstTimeUserLogin(userDetails.Email,false);
            }

            await base.OnInitializedAsync();
        }

        public void GoToNextPage()
        {
            bool forceLoad = true;
            string pagelink = "/logout";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
