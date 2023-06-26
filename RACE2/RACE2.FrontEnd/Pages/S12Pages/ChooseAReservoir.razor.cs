using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.FrontEnd.FluxorImplementation.Stores;
using RACE2.FrontEnd.RACE2GraphQLSchema;
using System.Security.Claims;
using RACE2.FrontEnd.FluxorImplementation.Actions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace RACE2.FrontEnd.Pages.S12Pages
{
    public partial class ChooseAReservoir
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
        public IState<UserReservoirsState> UserReservoirsState { get; set; } = default!;
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
            ReservoirsLinkedToUser = UserReservoirsState.Value.UserReservoirs;
            List<string> reservoirNamesList = new List<string>();
            foreach (var reservoir in ReservoirsLinkedToUser)
            {
                reservoirNamesList.Add(reservoir.PublicName);
            }

            reservoirNames = reservoirNamesList.ToArray<string>();
           
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

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/annual-statements";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }

        private async Task BeginSignOut(MouseEventArgs args)
        {
            await SignOutManager.SetSignOutState();
            NavigationManager.NavigateTo("authentication/logout");
        }
    }
}
