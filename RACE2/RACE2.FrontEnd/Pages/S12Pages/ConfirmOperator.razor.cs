using Fluxor;
using Microsoft.AspNetCore.Components;
using RACE2.FrontEnd.FluxorImplementation.Actions;
using RACE2.FrontEnd.FluxorImplementation.Stores;
using RACE2.FrontEnd.RACE2GraphQLSchema;

namespace RACE2.FrontEnd.Pages.S12Pages
{
    public partial class ConfirmOperator
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

        protected override async void OnInitialized()
        {
            var currentUser = CurrentUserDetailState.Value.CurrentUserDetail;
            base.OnInitialized();
        }
        public async void GoToNextPage()
        {

        }

        public async void GoToSaveComebackLaterPage()
        {

        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/reservoir-details";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
