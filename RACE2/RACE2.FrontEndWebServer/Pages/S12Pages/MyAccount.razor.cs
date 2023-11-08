using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using RACE2.DataModel;
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
        public IUserService userService { get; set; } = default!;

        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationStateTask { get; set; }
        //private int UserId { get; set; } = 0;
        private string UserName { get; set; } = "Unknown";
        private UserDetail UserDetail { get; set; } = new UserDetail();
        private Address UserAddress { get; set; }=new Address();

        private string changePasswordUrl = "";
        protected override async void OnInitialized()
        {
            try 
            { 
                changePasswordUrl = _config["RACE2SecurityProviderURL"] + "/Identity/Account/ChangeYourPassword";
                AuthenticationState authState = await AuthenticationStateTask; //AuthenticationStateProvider.GetAuthenticationStateAsync();
                UserName = authState.User.Claims.ToList().FirstOrDefault(c => c.Type == "name").Value;
                UserSpecificDto userDetails = await userService.GetUserByEmailID(UserName);
                UserAddress = userDetails.addresses[0];
                UserDetail = new UserDetail()
                {
                    UserName = UserName,
                    Id = userDetails.Id,
                    Email = userDetails.Email,
                    PhoneNumber = userDetails.PhoneNumber,
                    cFirstName = userDetails.cFirstName,
                    cLastName = userDetails.cLastName,
                    cMobile = userDetails.cMobile,
                    cAlternativePhone = userDetails.cAlternativePhone,
                    cAlternativeMobile = userDetails.cAlternativeMobile,
                    cAlternativeEmergencyPhone = userDetails.cAlternativeEmergencyPhone
                };
                await InvokeAsync(() =>
                {
                    StateHasChanged();
                });                
            }
            catch (Exception ex)
            {
                Serilog.Log.Logger.ForContext("User", UserName).ForContext("Application", "FrontEndWebServer").ForContext("Method", "MyAccount OnInitializedAsync").Fatal("Error getting data from backend services : " + ex.Message);
                throw new ApplicationException("Error loading my account page.");
            }
            finally
            {
                await base.OnInitializedAsync();
            }
        }

        private void goback()
        {
            bool forceLoad = true;
            NavigationManager.NavigateTo("/annual-statements", forceLoad);
        }
    }
}
