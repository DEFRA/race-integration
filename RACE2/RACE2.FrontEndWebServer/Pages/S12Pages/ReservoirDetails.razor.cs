using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.FrontEndWebServer.FluxorImplementation.Stores;
using RACE2.FrontEndWebServer.FluxorImplementation.Actions;
using RACE2.FrontEndWebServer.RACE2GraphQLSchema;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using RACE2.Dto;
using Microsoft.AspNetCore.Components.Forms;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class ReservoirDetails
    {
        [Inject]
        public SignOutSessionStateManager SignOutManager { get; set; } = default!;
        [Inject]
        public RACE2GraphQLClient client { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IState<CurrentReservoirState> CurrentReservoirState { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        public Reservoir CurrentReservoir { get; set; } = new Reservoir();
        public string ReservoirName { get; set; } = default!;
        private int UserId { get; set; } = 0;
        private string UserName { get; set; } = "Unknown";
        private UserDetail UserDetail { get; set; }

        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            UserName = authState.User.Claims.ToList().FirstOrDefault(c => c.Type == "name").Value;
            //var userDetails = await client.GetUserByEmailID.ExecuteAsync(UserName);
            //UserId = userDetails!.Data!.UserByEmailID.Id;
            var userDetails = await client.GetUserByEmailID.ExecuteAsync(UserName);
            UserId = userDetails!.Data!.UserByEmailID.Id;
            UserDetail = new UserDetail()
            {
                UserName = UserName,
                Id = UserId,
                Email = userDetails!.Data!.UserByEmailID.Email
            };
            base.OnInitialized();
        }

        public async void GoToNextPage()
        {
            bool forceLoad = false;
            NavigationManager.NavigateTo("/confirm-operator", forceLoad);
        }

        public async void GoToSaveComebackLaterPage()
        {

        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/choose-a-reservoir";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
        private async Task BeginSignOut(MouseEventArgs args)
        {
            await SignOutManager.SetSignOutState();
            NavigationManager.NavigateTo("authentication/logout");
        }
        private async void DownloadReportTemplate()
        {
            var blobName = "s12ReportTemplate" + "_" + UserName + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".docx";
            var result1 = await client.WriteContentToBlob.ExecuteAsync(blobName, CurrentReservoir.PublicName);
            var result2 = await client.DownloadBlobToLocalFile.ExecuteAsync(blobName, "c:\\temp\\" + blobName);
        }

        private async void UploadCompletedReport()
        {
            //    var blobName = "s12ReportComplete" + "_" + CurrentUser.UserName + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".docx";
            //    //var filename = "c:\\temp\\s12ReportComplete_kriss.sahoo@capgemini.com_1172023.docx";
            //    var filename = selectedFolder+selectedFile;

            //    if (!String.IsNullOrWhiteSpace(filename))
            //    {
            //        var result1 = await client.UploadToBlobFromLocalFile.ExecuteAsync(blobName, filename);
            //    }


            bool forceLoad = false;
            //string pagelink = "/upload-s12report";
            string pagelink = "/upload-multiple-s12reports";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
