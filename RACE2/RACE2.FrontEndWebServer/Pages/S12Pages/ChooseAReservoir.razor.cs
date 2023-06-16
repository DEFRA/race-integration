﻿using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.FrontEndWebServer.FluxorImplementation.Stores;
using RACE2.FrontEndWebServer.RACE2GraphQLSchema;
using System.Security.Claims;
using RACE2.FrontEndWebServer.FluxorImplementation.Actions;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class ChooseAReservoir
    {
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
        string? SelectedReservoirName;
        bool? IsLoggedIn;
        string? filter;
        private string[] filteredReservoirNames;
        private List<Reservoir> ReservoirsLinkedToUser { get; set; } =new List<Reservoir>();
        private IEnumerable<Claim> UserClaims { get; set; }
        private string UserName { get; set; } = "Unknown";
        private int UserId { get; set; } = 0;

        private string[] reservoirNames = Array.Empty<String>();

        protected override async void OnInitialized()
        {            
            var currentUser = CurrentUserDetailState.Value.CurrentUserDetail;
            var results = await client.GetReservoirsByUserId.ExecuteAsync(currentUser.Id);
            List<string> reservoirNamesList = new List<string>();
            var reservoirs = results!.Data!.ReservoirsByUserId;

            foreach (var rn in reservoirs)
            {
                reservoirNamesList.Add(rn.PublicName);
                var r = new Reservoir()
                {
                    RaceReservoirId = rn.RaceReservoirId,
                    PublicName = rn.PublicName,
                    NearestTown = rn.NearestTown,
                    GridReference = rn.GridReference
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
            reservoirNames = reservoirNamesList.ToArray<string>();
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            //if (authState.User.Identity.Name is not null)
            //{
            //    UserName = authState.User.Identity.Name;
            //    UserClaims = authState.User.Claims;
            //}
            //var userDetails = await client.GetUserByEmailID.ExecuteAsync(UserName);
            //UserId = userDetails!.Data!.UserByEmailID.Id;
            //var results = await client.GetReservoirsByUserId.ExecuteAsync(UserId);
            //List<string> reservoirNamesList = new List<string>();
            //var reservoirs = results!.Data!.ReservoirsByUserId;

            //foreach (var rn in reservoirs)
            //{
            //    reservoirNamesList.Add(rn.Public_name);
            //    var r = new Reservoir()
            //    {
            //        race_reservoir_id = rn.Race_reservoir_id,
            //        public_name = rn.Public_name,
            //        NearestTown = rn.NearestTown,
            //        grid_reference = rn.Grid_reference
            //    };
            //    r.address = new Address()
            //    {
            //        AddressLine1 = rn.Address.AddressLine1,
            //        AddressLine2 = rn.Address.AddressLine2,
            //        Town = rn.Address.Town,
            //        County = rn.Address.County,
            //        Postcode = rn.Address.Postcode
            //    };
            //    ReservoirsLinkedToUser.Add(r);
            //}
            //reservoirNames = reservoirNamesList.ToArray<string>();

            base.OnInitialized();
        }
        
        private async Task<IEnumerable<string>> SearchValues(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(1);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
            {                
                return reservoirNames;
            }
            return reservoirNames.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }

        async Task HandleInput(ChangeEventArgs e)
        {
            filter = e.Value?.ToString();
            if (filter?.Length > 0)
            {
                filteredReservoirNames = reservoirNames.Where(r => r.Contains(filter)).ToArray();
                //customers = await http.GetFromJsonAsync<List<Customer>>("https://localhost:5002/api/companyfilter/" + filter.ToString());
            }
            else
            {
                filteredReservoirNames = Array.Empty<string>();
                SelectedReservoirName = null;
            }
        }

        void SelectReservoir(string reservoir)
        {
            SelectedReservoirName = reservoir;
            CurrentReservoir.PublicName = reservoir;
            filteredReservoirNames = null;
        }

        public async void GoToNextPage()
        {
            SelectedReservoirName = CurrentReservoir.PublicName;
            var selectedReservoir = ReservoirsLinkedToUser.FirstOrDefault(r => r.PublicName == SelectedReservoirName);
            var action = new StoreReservoirAction(selectedReservoir);
            Dispatcher.Dispatch(action);
            bool forceLoad = false;
            string pagelink = "/reservoir-details";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }

        public async void GoToSaveComebackLaterPage()
        {

        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/annual-statements";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }        
    }
}