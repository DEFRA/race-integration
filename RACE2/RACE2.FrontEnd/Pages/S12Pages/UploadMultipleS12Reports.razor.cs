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
using System;
//using RACE2.FrontEnd.RACE2GraphQLSchema;

namespace RACE2.FrontEnd.Pages.S12Pages
{
    public partial class UploadMultipleS12Reports
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
        IBrowserFile[] uploadedFiles { get; set; }
        private int NoOfFilesChosen;

        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = CurrentUserDetailState.Value.CurrentUserDetail;
            CurrentReservoir = CurrentReservoirState.Value.CurrentReservoir;
            base.OnInitialized();
        }
        private void OnInputFileChange(InputFileChangeEventArgs e)
        {
            uploadedFiles= e.GetMultipleFiles().ToArray();
            NoOfFilesChosen = e.GetMultipleFiles().Count();
            //if (NoOfFilesChosen == 0) return;
            this.StateHasChanged();
        }
        private async void UploadCompletedReport()
        {
            for (int i = 0; i < NoOfFilesChosen; i++)
            {

                var blobName = "s12ReportComplete" + "_" + CurrentUser.UserName + "_" + DateTime.Now.Day.ToString("D2") + DateTime.Now.Month.ToString("D2") + DateTime.Now.Year.ToString("D4") +"_"+(i + 1).ToString() + ".docx";

                var filename = uploadedFiles[i].Name;

                if (!String.IsNullOrWhiteSpace(filename))
                {
                    //var result1 = await client.UploadToBlobFromLocalFile.ExecuteAsync(blobName, filename);
                    var fileToUpload = new UploadFileInput();
                    var stream = uploadedFiles[i].OpenReadStream(968435456);
                    fileToUpload.File = new Upload(stream, filename);
                    fileToUpload.BlobName = blobName;
                    var result1 = await client.UploadFile.ExecuteAsync(fileToUpload);
                }
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
