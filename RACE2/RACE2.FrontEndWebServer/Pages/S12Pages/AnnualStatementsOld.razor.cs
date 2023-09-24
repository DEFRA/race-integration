﻿using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.FrontEndWebServer.FluxorImplementation.Actions;
using RACE2.FrontEndWebServer.FluxorImplementation.Stores;
using RACE2.Services;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class AnnualStatementsOld
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IState<CurrentUserDetailState> CurrentUserDetailState { get; set; } = default!;
        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;
        [Inject]
        public IUserService userService { get; set; } = default!;
        [Inject]
        public IReservoirService reservoirService { get; set; } = default!;
        bool? IsLoggedIn;
        private string UserName { get; set; } = "Unknown";
        private UserDetail UserDetail { get; set; } = default!;
        private List<Reservoir> ReservoirsLinkedToUser { get; set; } = new List<Reservoir>();
        private List<SubmissionStatusDTO> ReservoirStatusLinkedToUser { get; set; } = new List<SubmissionStatusDTO>();
        private List<SubmissionStatusDTO> ReservoirStatusLinkedToUserSubmitted { get; set; } = new List<SubmissionStatusDTO>();
        private List<SubmissionStatusDTO> ReservoirStatusLinkedToUserDraft { get; set; } = new List<SubmissionStatusDTO>();
        private IEnumerable<Claim> Claims { get; set; }

        protected async override Task OnInitializedAsync()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            UserName = authState.User.Claims.ToList().FirstOrDefault(c => c.Type == "name").Value;
            UserSpecificDto userDetails = await userService.GetUserByEmailID(UserName);
            UserDetail = new UserDetail()
            {
                UserName = UserName,
                Id = userDetails.Id,
                Email = userDetails.Email,
            };

            var resultsOfReservoirWithStatus = await reservoirService.GetReservoirStatusByEmail(UserDetail.Email);
            var reservoirStatusLinkedToUser = resultsOfReservoirWithStatus.ToList();
            foreach (var rs in reservoirStatusLinkedToUser)
            {
                var s = new SubmissionStatusDTO()
                {
                    PublicName = rs.PublicName,
                    SubmittedOn = new DateTime(rs.SubmittedOn.Year, rs.SubmittedOn.Month, rs.SubmittedOn.Day),
                    Status = rs.Status
                };
                ReservoirStatusLinkedToUser.Add(s);
            }
            ReservoirStatusLinkedToUserSubmitted = ReservoirStatusLinkedToUser.Where(st => st.Status.ToUpper() == "COMPLETE").ToList();
            ReservoirStatusLinkedToUserDraft = ReservoirStatusLinkedToUser.Where(st => st.Status.ToUpper() != "COMPLETE").ToList();
            var results = await reservoirService.GetReservoirsByUserId(userDetails.Id);

            var reservoirs = results.ToList();

            foreach (var rn in reservoirs)
            {
                var r = new Reservoir()
                {
                    Id = rn.Id,
                    RaceReservoirId = rn.RaceReservoirId,
                    PublicName = rn.PublicName,
                    NearestTown = rn.NearestTown,
                    GridReference = rn.GridReference,
                    OperatorType = rn.OperatorType
                };
                r.Address = new Address()
                {
                    AddressLine1 = rn.Address.AddressLine1,
                    AddressLine2 = rn.Address.AddressLine2,
                    Town = rn.Address.Town,
                    County = rn.Address.County,
                    Postcode = rn.Address.Postcode
                };
                ReservoirsLinkedToUser.Add(r);
            }
            var actionUserDetail = new StoreUserDetailAction(UserDetail);
            Dispatcher.Dispatch(actionUserDetail);

            var actionReservoirsLinkedToUser = new StoreUserReservoirsAction(ReservoirsLinkedToUser);
            Dispatcher.Dispatch(actionReservoirsLinkedToUser);

            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
            base.OnInitializedAsync();
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            
        }
        public async void GoToNextPage()
        {
            var u = CurrentUserDetailState.Value.CurrentUserDetail;
            bool forceLoad = false;
            string pagelink = "/choose-a-reservoir";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }

        private void gotoPage(SubmissionStatusDTO reservoirStatus)
        {
            var reservoir = ReservoirsLinkedToUser.Where(s => s.PublicName == reservoirStatus.PublicName).FirstOrDefault();
            var action = new StoreReservoirAction(reservoir);
            Dispatcher.Dispatch(action);
            bool forceLoad = false;
            string pagelink = "/reservoir-details";
            if (reservoirStatus.Status.ToUpper() == "DRAFT SENT")
            {
                pagelink = "/s12-statement-confirmation-draft-sent";
            }
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }

        private void gotoSubmissionPage(SubmissionStatusDTO reservoirStatus)
        {
            var reservoir = ReservoirsLinkedToUser.Where(s => s.PublicName == reservoirStatus.PublicName).FirstOrDefault();
            var action = new StoreReservoirAction(reservoir);
            Dispatcher.Dispatch(action);
            bool forceLoad = false;
            string pagelink = "/s12-statement-confirmation";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
        private void Dispose()
        {
            this.Dispose(true);
        }

        public string text1 = "";
        public string text2 = "";

        public bool IsEnabled = false;

        public async Task OnTabChanged(Tab tab)
        {
            text1 = $"Tab value: {tab.Value}";
            text2 = $"Tab text: {tab.Text}";
        }        
    }
}