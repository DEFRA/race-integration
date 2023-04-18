using Fluxor;
using Microsoft.AspNetCore.Components;
using RACE2.Dto;
using RACE2.FrontEnd.Features.CurrentUserDetail.Store;
//using RACE2.FrontEnd.RACE2GraphQLSchema;

namespace RACE2.FrontEnd.Components
{
    public partial class NewReservoir
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

        public ReservoirDetailsDTO newReservoir = new ReservoirDetailsDTO();

        protected override void OnInitialized()
        {
            //newReservoir = CurrentUserDetailState.ReservoirDetailsDTO;
        }
    }
}
