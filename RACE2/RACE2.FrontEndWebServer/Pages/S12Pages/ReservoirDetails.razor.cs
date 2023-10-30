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
    public partial class ReservoirDetails
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IJSRuntime jsRuntime { get; set; } = default!;
        [Inject]
        public IUserService userService { get; set; } = default!;
        [Inject]
        public IBlobStorageService blobStorageService { get; set; } = default!;
        [Inject]
        public IOpenXMLUtilitiesService openXMLUtilitiesService { get; set; } = default!;

        public Reservoir CurrentReservoir { get; set; } = new Reservoir();
        public string ReservoirName { get; set; } = default!;
        //private int UserId { get; set; } = 0;
        private string UserName { get; set; } = "Unknown";
        private UserDetail UserDetail { get; set; }
        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationStateTask { get; set; }
        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateTask; // AuthenticationStateProvider.GetAuthenticationStateAsync();
            UserName = authState.User.Claims.ToList().FirstOrDefault(c => c.Type == "name").Value;
            UserSpecificDto userDetails = await userService.GetUserByEmailID(UserName);
            UserDetail = new UserDetail()
            {
                UserName = UserName,
                Id = userDetails.Id,
                Email = userDetails.Email,
                cFirstName = userDetails.cFirstName,
                cLastName = userDetails.cLastName
            };
            CurrentReservoir = new Reservoir();
            base.OnInitialized();
        }

        public async void GoToNextPage()
        {
            bool forceLoad = false;
            NavigationManager.NavigateTo("/confirm-operator", forceLoad);
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/annual-statements";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
        private async void DownloadReportTemplate()
        {
            var blobName = "S12ReportTemplate.docx";
            Stream response = await blobStorageService.GetBlobFileStream(blobName);
            S12PrePopulationFields s12PrePopulationFields = new S12PrePopulationFields();
            s12PrePopulationFields.ReservoirName = CurrentReservoir.PublicName;
            s12PrePopulationFields.SupervisingEngineerName = UserDetail.cFirstName + " " + UserDetail.cLastName;
            s12PrePopulationFields.ReservoirNearestTown = CurrentReservoir.NearestTown != null? CurrentReservoir.NearestTown:"";
            s12PrePopulationFields.ReservoirGridRef = CurrentReservoir.GridReference != null ? CurrentReservoir.GridReference : "";
            MemoryStream processedStream = openXMLUtilitiesService.SearchAndReplace(response, s12PrePopulationFields);
            processedStream.Position = 0;
            var streamRef = new DotNetStreamReference(stream: processedStream);
            await jsRuntime.InvokeVoidAsync("downloadFileFromStream", blobName, streamRef);
        }

        private async void UploadCompletedReport()
        {
            bool forceLoad = false;
            //string pagelink = "/upload-s12report";
            string pagelink = "/upload-multiple-s12reports";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
