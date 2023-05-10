using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.FrontEnd.RACE2GraphQLSchema;
using System.Security.Claims;

namespace RACE2.FrontEnd.Components
{
    public partial class AnnualStatements
    {
        [Inject]
        public RACE2GraphQLClient client { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        string CurrentUserEmail;
        bool? IsLoggedIn;
        private IEnumerable<Claim> UserClaims { get; set; }
        private string UserName { get; set; } = "Unknown";
        private List<Reservoir> ReservoirsLinkedToUser { get; set; } = new List<Reservoir>();
        private int UserId { get; set; } = 0;
        private string[] reservoirNames = Array.Empty<String>();

        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            if (authState.User.Identity.Name is not null)
            {
                UserName = authState.User.Identity.Name;
                UserClaims = authState.User.Claims;
            }
            var userDetails = await client.GetUserByEmailID.ExecuteAsync(UserName);
            UserId = userDetails!.Data!.UserByEmailID.Id;
            var results = await client.GetReservoirsByUserId.ExecuteAsync(UserId);
            List<string> reservoirNamesList = new List<string>();
            var reservoirs = results!.Data!.ReservoirsByUserId;

            foreach (var rn in reservoirs)
            {
                reservoirNamesList.Add(rn.Public_name);
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
            reservoirNames = reservoirNamesList.ToArray<string>();
        }

        public async void GoToNextPage()
        {
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
