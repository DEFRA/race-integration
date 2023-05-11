using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.FrontEnd.FluxorImplementation.Stores;
using RACE2.FrontEnd.RACE2GraphQLSchema;
using System.Security.Claims;
using RACE2.FrontEnd.FluxorImplementation.Actions;
using Microsoft.AspNetCore.Components;

namespace RACE2.FrontEnd.Components
{
    public partial class ReservoirDetails
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
        public string ReservoirName { get; set; } = default!;

        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            base.OnInitialized();
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/choose-a-reservoir";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                CurrentUserDetailState.StateChanged += StateChanged;
                CurrentReservoirState.StateChanged += StateChanged;
            }
        }
        public void StateChanged(object sender, EventArgs args)
        {
            InvokeAsync(StateHasChanged);
        }

        void IDisposable.Dispose()
        {
            CurrentUserDetailState.StateChanged -= StateChanged;
            CurrentReservoirState.StateChanged -= StateChanged;
        }
    }
}
