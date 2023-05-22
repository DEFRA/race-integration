﻿using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.FrontEndWeb.FluxorImplementation.Actions;
using RACE2.FrontEndWeb.FluxorImplementation.Stores;
using RACE2.FrontEndWeb.RACE2GraphQLSchema;
using System.Security.Claims;

namespace RACE2.FrontEndWeb.Pages.S12Pages
{
    public partial class AnnualStatements
    {
        [Inject]
        public RACE2GraphQLClient client { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IState<CurrentUserDetailState> CurrentUserDetailState { get; set; } = default!;
        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        string CurrentUserEmail;
        bool? IsLoggedIn;
        private IEnumerable<Claim> UserClaims { get; set; }
        private int UserId { get; set; } = 0;
        private string UserName { get; set; } = "Unknown";
        private UserDetail UserDetail { get; set; } 
        private List<Reservoir> ReservoirsLinkedToUser { get; set; } = new List<Reservoir>();


        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            UserName = authState.User.Claims.ToArray()[6].Value;
            //if (authState.User.Identity.Name is not null)
            //{
            //    UserName = authState.User.Identity.Name;
            //    UserClaims = authState.User.Claims;
            //}
            var userDetails = await client.GetUserByEmailID.ExecuteAsync(UserName);
            UserId = userDetails!.Data!.UserByEmailID.Id;
            UserDetail = new UserDetail()
            {
                UserName= UserName,
                Id= UserId,
                Email= userDetails!.Data!.UserByEmailID.Email
            };
            var results = await client.GetReservoirsByUserId.ExecuteAsync(UserId);
            List<string> reservoirNamesList = new List<string>();
            var reservoirs = results!.Data!.ReservoirsByUserId;

            foreach (var rn in reservoirs)
            {
                var r = new Reservoir()
                {
                    race_reservoir_id = rn.Race_reservoir_id,
                    public_name = rn.Public_name,
                    NearestTown = rn.NearestTown,
                    grid_reference = rn.Grid_reference
                };
                r.address = new Address()
                {
                    AddressLine1 = rn.Address.AddressLine1,
                    AddressLine2 = rn.Address.AddressLine2,
                    Town = rn.Address.Town,
                    County = rn.Address.County,
                    Postcode = rn.Address.Postcode
                };
                ReservoirsLinkedToUser.Add(r);
            }
            var action = new StoreUserDetailAction(UserDetail);
            Dispatcher.Dispatch(action);

            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
            base.OnInitialized();
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