using Fluxor;
using Microsoft.AspNetCore.Components;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.FrontEnd.FluxorImplementation.Actions;
using RACE2.FrontEnd.FluxorImplementation.Stores;
using RACE2.FrontEnd.RACE2GraphQLSchema;
using RACE2.FrontEnd.Utilities;
using StrawberryShake;
using System.Reactive.Threading.Tasks;

namespace RACE2.FrontEnd.Pages.S12Pages
{
    public partial class NewOperatorDetails
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public RACE2GraphQLClient client { get; set; } = default!;
        [Inject]
        public IState<CurrentUserDetailState> CurrentUserDetailState { get; set; } = default!;
        [Inject]
        public IState<CurrentReservoirState> CurrentReservoirState { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        public List<OperatorDTO> OperatorDetails { get; set; }

        public UserDetail UserDetail { get; set; }

        private YesNoClass _yesnoOption = new YesNoClass();
    }
}
