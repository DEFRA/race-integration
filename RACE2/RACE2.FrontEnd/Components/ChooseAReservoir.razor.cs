using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.FrontEnd.Features.CurrentUserDetail.Store;
using RACE2.FrontEnd.RACE2GraphQLSchema;

namespace RACE2.FrontEnd.Components
{
    public partial class ChooseAReservoir
    {
        [Inject]
        public RACE2GraphQLClient client { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IState<CurrentUserDetailState> State { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        public CurrentUserDetailState CurrentUserDetailState => State.Value;

        public Reservoir CurrentReservoir { get; set; } = new Reservoir();
        string? SelectedReservoirName;
        string CurrentUserEmail;
        bool? IsLoggedIn;
        string? filter;
        private string[] filteredReservoirNames;

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
            //SelectedReservoirName = CurrentUserDetailState.CurrentReservoir.public_name;
            CurrentUserEmail = CurrentUserDetailState.CurrentUserDetail.Email;
            var results = await client.GetReservoirsByUserEmailId.ExecuteAsync("kcsahoo@gmail.com");
            List<string> reservoirNamesList = new List<string>();
            foreach (var rn in results!.Data!.ReservoirsByUserEmailId.Reservoirs)
            {
                reservoirNamesList.Add(rn.Public_name);
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
                return reservoirNames;
            return reservoirNames.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }

        public void GoToNextPage()
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
                filteredReservoirNames = null;
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
            string pagelink = "/enter-email";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
