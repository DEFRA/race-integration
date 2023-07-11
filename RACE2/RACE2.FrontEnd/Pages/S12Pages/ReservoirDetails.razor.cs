using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.FrontEnd.FluxorImplementation.Stores;
using RACE2.FrontEnd.FluxorImplementation.Actions;
using RACE2.FrontEnd.RACE2GraphQLSchema;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using RACE2.Dto;
using Microsoft.AspNetCore.Components.Forms;

namespace RACE2.FrontEnd.Pages.S12Pages
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
        public IState<CurrentUserDetailState> CurrentUserDetailState { get; set; } = default!;
        [Inject]
        public IState<CurrentReservoirState> CurrentReservoirState { get; set; } = default!;
        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        public Reservoir CurrentReservoir { get; set; } = new Reservoir();
        public UserDetail CurrentUser { get; set; } = new UserDetail();
        public string ReservoirName { get; set; } = default!;
        string Message = "No file(s) selected";
        IReadOnlyList<IBrowserFile> selectedFiles;
        string selectedFile = "";
        bool isUpload=false;

        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = CurrentUserDetailState.Value.CurrentUserDetail;
            CurrentReservoir = CurrentReservoirState.Value.CurrentReservoir;
            base.OnInitialized();
        }

        public async Task GoToNextPage()
        {
            ReservoirUpdateDetailsDTOInput updatedReservoir = new ReservoirUpdateDetailsDTOInput();
            updatedReservoir.Id = CurrentReservoir.Id;
            updatedReservoir.UserId = CurrentUser.Id;
            updatedReservoir.PublicName = CurrentReservoir.PublicName;
            updatedReservoir.GridReference= CurrentReservoir.GridReference;
            updatedReservoir.NearestTown= CurrentReservoir.NearestTown;
            var savedReservoir = await client.UpdateReservoir.ExecuteAsync(updatedReservoir);
            bool forceLoad = false;
            NavigationManager.NavigateTo("/confirm-operator", forceLoad);
        }

        private async Task BeginSignOut(MouseEventArgs args)
        {            
            await SignOutManager.SetSignOutState();
            NavigationManager.NavigateTo("authentication/logout");
        }

        private void OnInputFileChange(InputFileChangeEventArgs e)
        {
            selectedFiles = e.GetMultipleFiles();
            selectedFile = selectedFiles.FirstOrDefault().Name;
            Message = $"{selectedFiles.Count} file(s) selected";
            this.StateHasChanged();
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/annual-statements";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }

        private async void DownloadReportTemplate()
        {
            var blobName = "s12ReportTemplate" +"_" +CurrentUser.UserName + "_" + DateTime.Now.Day+ DateTime.Now.Month + DateTime.Now.Year +".docx";
            var result1 = await client.WriteContentToBlob.ExecuteAsync(blobName,CurrentReservoir.PublicName);
            var result2 = await client.DownloadBlobToLocalFile.ExecuteAsync(blobName, "c:\\temp\\"+blobName);
        }

        private async void UploadCompletedReport()
        {
            var blobName = "s12ReportComplete" + "_" + CurrentUser.UserName + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".docx";
            //var filename = "c:\\temp\\s12ReportComplete_kriss.sahoo@capgemini.com_1172023.docx";
            var filename = "c:\\temp\\"+selectedFile;
            if (!String.IsNullOrWhiteSpace(filename))
            {
                var result1 = await client.UploadToBlobFromLocalFile.ExecuteAsync(blobName, filename);
            }
        }

        private void changeReservoirDetailsName()
        {
            bool forceLoad = false;
            string pagelink = "/reservoir-details-change-name";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }

        private void changeReservoirDetailsNearestTown()
        {
            bool forceLoad = false;
            string pagelink = "/reservoir-details-change-nearesttown";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }

        private void changeReservoirDetailsGridReference()
        {
            bool forceLoad = false;
            string pagelink = "/reservoir-details-change-gridreference";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
