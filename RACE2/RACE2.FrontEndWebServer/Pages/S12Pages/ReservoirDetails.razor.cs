﻿using Fluxor;
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
    public partial class ReservoirDetails
    {
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
        [Inject]
        public IBlobStorageService blobStorageService { get; set; } = default!;
        [Inject]
        public IOpenXMLUtilitiesService openXMLUtilitiesService { get; set; } = default!;

        public Reservoir CurrentReservoir { get; set; } = new Reservoir();
        public string ReservoirName { get; set; } = default!;
        //private int UserId { get; set; } = 0;
        private string UserName { get; set; } = "Unknown";
        private UserDetail UserDetail { get; set; }

        protected override async void OnInitialized()
        {
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
            CurrentReservoir = CurrentReservoirState.Value.CurrentReservoir;
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
            s12PrePopulationFields.ReservoirName = CurrentReservoirState.Value.CurrentReservoir.PublicName;
            s12PrePopulationFields.SupervisingEngineerName = UserDetail.c_first_name + " " + UserDetail.c_last_name;
            s12PrePopulationFields.ReservoirNearestTown = CurrentReservoirState.Value.CurrentReservoir.NearestTown != null? CurrentReservoirState.Value.CurrentReservoir.NearestTown:"";
            s12PrePopulationFields.ReservoirGridRef = CurrentReservoirState.Value.CurrentReservoir.GridReference != null ? CurrentReservoirState.Value.CurrentReservoir.GridReference : "";
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
