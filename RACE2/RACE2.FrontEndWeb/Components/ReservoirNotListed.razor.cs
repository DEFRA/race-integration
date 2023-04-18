using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.FrontEndWeb.FluxorImplementation.Actions;
using RACE2.FrontEndWeb.FluxorImplementation.Stores;
using RACE2.FrontEndWeb.RACE2GraphQLSchema;

namespace RACE2.FrontEndWeb.Components
{
    public partial class ReservoirNotListed
    {

        [Inject]
        public RACE2GraphQLClient client { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IState<AppStore> State { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        public AppStore AppStore => State.Value;

        public ReservoirDetailsDTO newReservoir =new ReservoirDetailsDTO();

        protected override void OnInitialized()
        {
            newReservoir = AppStore.NewReservoirDetails;
        }

        bool forceLoad = true;
        public void GoToNextPage()
        {
            var action = new StoreNewReservoirAction(newReservoir);
            Dispatcher.Dispatch(action);
            NavigationManager.NavigateTo("/new-reservoir", forceLoad);
        }
    }
}
