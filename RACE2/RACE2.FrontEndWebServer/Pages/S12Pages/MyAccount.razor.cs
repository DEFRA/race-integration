using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using RACE2.DataModel;
using RACE2.FrontEndWebServer.FluxorImplementation.Stores;
using RACE2.FrontEndWebServer.FluxorImplementation.Actions;
using Microsoft.AspNetCore.Components.Web;
using RACE2.Dto;
using Microsoft.AspNetCore.Components.Forms;
using RACE2.Services;
using System.IO;
using System.IO.Pipes;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Azure;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class MyAccount
    {
        [Inject]
        public IConfiguration _config { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IState<CurrentReservoirState> CurrentReservoirState { get; set; } = default!;
        [Inject]
        public IState<CurrentUserDetailState> CurrentUserDetailState { get; set; } = default!;
        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;
        [Inject]
        public IJSRuntime jsRuntime { get; set; } = default!;
        [Inject]
        public IUserService userService { get; set; } = default!;
        //private int UserId { get; set; } = 0;
        private string UserName { get; set; } = "Unknown";
        private UserDetail UserDetail { get; set; } = new UserDetail();
        private Address UserAddress { get; set; }=new Address();

        private string changePasswordUrl = "";
        protected override async void OnInitialized()
        {
            changePasswordUrl = _config["RACE2SecurityProviderURL"] + "/Identity/Account/ChangeYourPassword";
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            UserName = authState.User.Claims.ToList().FirstOrDefault(c => c.Type == "name").Value;
            UserSpecificDto userDetails = await userService.GetUserByEmailID(UserName);
            UserDetail = new UserDetail()
            {
                UserName = UserName,
                Id = userDetails.Id,
                Email = userDetails.Email,
                c_first_name = userDetails.c_first_name,
                c_last_name = userDetails.c_last_name
            };
            UserAddress = userDetails.addresses[0];
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
            base.OnInitializedAsync();
        }

        public async void GoToNextPage()
        {
            bool forceLoad = true;
            NavigationManager.NavigateTo("/annual-statements", forceLoad);
        }

        private void goback()
        {
            bool forceLoad = true;
            NavigationManager.NavigateTo("/annual-statements", forceLoad);
        }
    }
}
