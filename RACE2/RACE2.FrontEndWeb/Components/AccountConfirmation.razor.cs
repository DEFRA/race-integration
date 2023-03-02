using Fluxor;
using Microsoft.AspNetCore.Components;
using RACE2.FrontEndWeb.FluxorImplementation.Actions;
using RACE2.FrontEndWeb.FluxorImplementation.Stores;

namespace RACE2.FrontEndWeb.Components
{
    public partial class AccountConfirmation
    {
        [Inject]
        public IState<AppStore> State { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        public AppStore AppStore => State.Value;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        private void changepassword()
        {
            bool forceLoad = false;
            string pagelink = "/change-password";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
        public async Task GoToNextPage()
        {
            bool forceLoad = false;
            string pagelink = "/choose-a-reservoir";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/change-password";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
