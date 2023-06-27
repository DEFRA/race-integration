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

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/annual-statements";
            NavigationManager.NavigateTo(pagelink, forceLoad);
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
