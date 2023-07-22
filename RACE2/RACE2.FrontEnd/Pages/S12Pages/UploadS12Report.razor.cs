using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.FrontEnd.FluxorImplementation.Actions;
using RACE2.FrontEnd.FluxorImplementation.Stores;
using RACE2.FrontEnd.RACE2GraphQLSchema;
using StrawberryShake;
//using RACE2.FrontEnd.RACE2GraphQLSchema;

namespace RACE2.FrontEnd.Pages.S12Pages
{
    public partial class UploadS12Report
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
        public UserDetail CurrentUser { get; set; } = new UserDetail();
        public Reservoir CurrentReservoir { get; set; } = new Reservoir();
        public string ReservoirName { get; set; } = default!;
        string selectedFile = "";
        string selectedFolder = "";
        Stream seletedFileContent { get; set; }
        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = CurrentUserDetailState.Value.CurrentUserDetail;
            CurrentReservoir = CurrentReservoirState.Value.CurrentReservoir;
            base.OnInitialized();
        }
        private void OnInputFileChange(InputFileChangeEventArgs e)
        {
            selectedFile = e.File.Name;
            seletedFileContent = e.File.OpenReadStream();
            if (selectedFile == null) return;
            this.StateHasChanged();
        }
        private async void UploadCompletedReport()
        {
            var s = seletedFileContent;
            var blobName = "s12ReportComplete" + "_" + CurrentUser.UserName + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".docx";
            var filename = selectedFile;

            if (!String.IsNullOrWhiteSpace(filename))
            {
                //var result1 = await client.UploadToBlobFromLocalFile.ExecuteAsync(blobName, filename);
                var fileToUpload = new UploadFileInput();
                fileToUpload.File = new Upload(seletedFileContent, filename);
                fileToUpload.BlobName = blobName;
                var result1 = await client.UploadFile.ExecuteAsync(fileToUpload);
            }
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/reservoir-details";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }        
    }
}
