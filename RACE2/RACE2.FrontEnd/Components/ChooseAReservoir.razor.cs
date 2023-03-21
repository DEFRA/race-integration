using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.FrontEnd.RACE2GraphQLSchema;
using System.Security.Claims;

namespace RACE2.FrontEnd.Components
{
    public partial class ChooseAReservoir
    {
        [Inject]
        public RACE2GraphQLClient client { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        public Reservoir CurrentReservoir { get; set; } = new Reservoir();
        string? SelectedReservoirName;
        string CurrentUserEmail;
        bool? IsLoggedIn;
        string? filter;
        private string[] filteredReservoirNames;
        private IEnumerable<Claim> UserClaims { get; set; }
        private string UserName { get; set; } = "Unknown";

        private string[] reservoirNames = Array.Empty<String>();
        //    {
        //    "River Foss Flood Storage Reservoir",
        //    "River Nar Flood Storage Area",
        //    "River Park Pond",
        //    "River Rase North Branch",
        //    "River Rase South Branch",
        //    "Rockingham Reservoir"
        //};

        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            if (authState.User.Identity.Name is not null)
            {
                UserName = authState.User.Identity.Name;
                UserClaims = authState.User.Claims;
            }
            //SelectedReservoirName = CurrentUserDetailState.CurrentReservoir.public_name;
            var results = await client.GetReservoirsByUserEmailId.ExecuteAsync(UserName);
            List<string> reservoirNamesList = new List<string>();
            foreach (var rn in results!.Data!.ReservoirsByUserEmailId.Reservoirs)
            {
                reservoirNamesList.Add(rn.Public_name);
            }
            reservoirNames = reservoirNamesList.ToArray<string>();
        }

        private async Task<IEnumerable<string>> SearchValues(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(1);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return reservoirNames;
            return reservoirNames.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }

        public async void GoToNextPage()
        {
            bool forceLoad = false;
            string pagelink = "/reservoir-details";
            NavigationManager.NavigateTo(pagelink, forceLoad);
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
            CurrentReservoir.public_name = reservoir;
            filteredReservoirNames = null;
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
