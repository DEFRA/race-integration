using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.FrontEndWeb.FluxorImplementation.Actions;
using RACE2.FrontEndWeb.FluxorImplementation.Stores;
using RACE2.FrontEndWeb.RACE2GraphQLSchema;

namespace RACE2.FrontEndWeb.Components
{
    public partial class ChooseAReservoir
    {
        [Inject]
        public RACE2GraphQLClient client { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IPasswordHasher<UserDetail> passwordHasher { get; set; } = default!;

        [Inject]
        public IState<AppStore> State { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        public AppStore AppStore => State.Value;
        public Reservoir CurrentReservoir { get; set; } = new Reservoir();
        string? SelectedReservoirName;
        int CurrentUserId;
        bool? IsLoggedIn;
        string? filter;
        private string[] filteredReservoirNames;

        private string[] reservoirNames=Array.Empty<String>();
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
            base.OnInitialized();
            SelectedReservoirName = AppStore.CurrentReservoir.PublicName;
            IsLoggedIn = AppStore.IsLoggedIn;
            CurrentUserId = AppStore.CurrentUserDetail.Id;
            var results = await client.GetReservoirsByUserId.ExecuteAsync(CurrentUserId);
            List<string> reservoirNamesList = new List<string>();
            foreach (var rn in results!.Data!.ReservoirsByUserId.Reservoirs)
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

        public void GoToNextPage()
        {
            var action = new StoreReservoirAction(CurrentReservoir);
            Dispatcher.Dispatch(action);
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
            CurrentReservoir.PublicName = reservoir;
            var action = new StoreReservoirAction(CurrentReservoir);
            Dispatcher.Dispatch(action);
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
