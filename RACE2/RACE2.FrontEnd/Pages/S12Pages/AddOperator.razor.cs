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
    public partial class AddOperator
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

        public List<OperatorDTO> OperatorDetails { get; set; }

        public UserDetail UserDetail { get; set; }

        private NewOperatorDetail NewOperatorDetail=new NewOperatorDetail();

        public async void GoToNextPage()
        {
            bool forceLoad = false;

            NavigationManager.NavigateTo("/alternate-supervising-engineer", forceLoad);
            
        }

        public async void GoToSaveComebackLaterPage()
        {

        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/confirm-operator";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }

        private async Task BeginSignOut(MouseEventArgs args)
        {
            await SignOutManager.SetSignOutState();
            NavigationManager.NavigateTo("authentication/logout");
        }
    }
}

public class NewOperatorDetail
{
    public string  Name { get; set; }
    public string AddressLine1 { get; set; }
    public string Town { get; set; }
    public string Postcode { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
