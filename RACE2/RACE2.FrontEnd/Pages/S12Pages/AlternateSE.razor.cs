using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
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
    public partial class AlternateSE
    {
        [Inject]
        public SignOutSessionStateManager SignOutManager { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        private RACE2GraphQLClient client { get; set; } = default!;
        [Inject]
        public IState<CurrentUserDetailState> CurrentUserDetailState { get; set; } = default!;
        [Inject]
        public IState<CurrentReservoirState> CurrentReservoirState { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        private YesNoClass _yesnoOption = new YesNoClass();

        public async void GoToNextPage()
        {
            var value = _yesnoOption.YesNoOptions.ToString();

            bool forceLoad = false;

            if (value == "Yes")
            {
                NavigationManager.NavigateTo("/add-alternate-supervising-engineer/", forceLoad);
            }
            else
            {
                NavigationManager.NavigateTo("/operator-maintenance-actions", forceLoad);
            }
        }

        private async Task BeginSignOut(MouseEventArgs args)
        {
            await SignOutManager.SetSignOutState();
            NavigationManager.NavigateTo("authentication/logout");
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/confirm-operator";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}

