using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.FrontEndWebServer.FluxorImplementation.Actions;
using RACE2.FrontEndWebServer.FluxorImplementation.Stores;
using RACE2.FrontEndWebServer.RACE2GraphQLSchema;
using StrawberryShake;
//using RACE2.FrontEnd.RACE2GraphQLSchema;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class UploadS12Report
    {
        [Inject]
        public RACE2GraphQLClient client { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;
        private int UserId { get; set; } = 0;
        private string UserName { get; set; } = "Unknown";
        private UserDetail UserDetail { get; set; }
        public Reservoir CurrentReservoir { get; set; } = new Reservoir();
        public string ReservoirName { get; set; } = default!;
        string selectedFile = "";
        string selectedFolder = "";
        Stream selectedFileContent { get; set; }
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
            selectedFile = e.File.Name;
            selectedFileContent = e.File.OpenReadStream();
            if (selectedFile == null) return;
            this.StateHasChanged();
        }
        private async void UploadCompletedReport()
        {
            var s = selectedFileContent;
            var blobName = "s12ReportComplete" + "_" + UserName + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".docx";
            var filename = selectedFile;

            if (!String.IsNullOrWhiteSpace(filename))
            {
                //var result1 = await client.UploadToBlobFromLocalFile.ExecuteAsync(blobName, filename);
                var fileToUpload = new UploadFileInput();
                fileToUpload.File = new Upload(selectedFileContent, filename);
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
