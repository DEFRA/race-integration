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
        public string ReservoirName { get; set; } = default!;

        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var currentUser = CurrentUserDetailState.Value.CurrentUserDetail;
            var currentReservoir = CurrentReservoirState.Value.CurrentReservoir;
            base.OnInitialized();
        }

        public async void GoToNextPage()
        {
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
            string pagelink = "/choose-a-reservoir";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }        
    }
}
