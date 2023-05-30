using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.FrontEndWebServer.FluxorImplementation.Stores;
//using RACE2.FrontEnd.RACE2GraphQLSchema;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class ReservoirNotListed
    {

        //[Inject]
        //public RACE2GraphQLClient client { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IState<CurrentUserDetailState> State { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        public CurrentUserDetailState CurrentUserDetailState => State.Value;

        public ReservoirDetailsDTO newReservoir =new ReservoirDetailsDTO();

        protected override void OnInitialized()
        {
            //newReservoir = CurrentUserDetailState.NewReservoirDetails;
            base.OnInitialized();
        }

        bool forceLoad = true;
        public void GoToNextPage()
        {
            NavigationManager.NavigateTo("/new-reservoir", forceLoad);
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/choose-a-reservoir";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
