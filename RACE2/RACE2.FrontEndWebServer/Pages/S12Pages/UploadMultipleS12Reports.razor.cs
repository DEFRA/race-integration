using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.FrontEndWebServer.FluxorImplementation.Actions;
using RACE2.FrontEndWebServer.FluxorImplementation.Stores;
using RACE2.FrontEndWebServer.RACE2GraphQLSchema;
using StrawberryShake;
using System;
//using RACE2.FrontEnd.RACE2GraphQLSchema;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
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
        public IDispatcher Dispatcher { get; set; } = default!;
        private int UserId { get; set; } = 0;
        private string UserName { get; set; } = "Unknown";
        private UserDetail UserDetail { get; set; }
        IBrowserFile[] uploadedFiles { get; set; }
        private int NoOfFilesChosen;

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

                var blobName = "s12ReportComplete" + "_" + UserName + "_" + DateTime.Now.Day.ToString("D2") + DateTime.Now.Month.ToString("D2") + DateTime.Now.Year.ToString("D4") +"_"+(i + 1).ToString() + ".docx";

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
