using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.FrontEnd.FluxorImplementation.Actions;
using RACE2.FrontEnd.FluxorImplementation.Stores;
using RACE2.FrontEnd.RACE2GraphQLSchema;
using RACE2.FrontEnd.Utilities;
using StrawberryShake;
using System.Reactive.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace RACE2.FrontEnd.Pages.S12Pages
{
    public partial class ConfirmOperator
    {
        [Inject]
        public SignOutSessionStateManager SignOutManager { get; set; } = default!;
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

        protected override async void OnInitialized()
        {
            UserDetail = CurrentUserDetailState.Value.CurrentUserDetail;
            var currentReservoir = CurrentReservoirState.Value.CurrentReservoir;
            var operatorDetailsList = await client.GetOperatorsforReservoir.ExecuteAsync(currentReservoir.Id, currentReservoir.OperatorType);
            var OperatorDetailsList = operatorDetailsList!.Data!.OperatorsforReservoir.ToList();
            foreach (var o in OperatorDetailsList)
            {
                if (OperatorDetails is null)
                {
                    OperatorDetails = new List<OperatorDTO>();
                }
                var operatorDTO = new OperatorDTO();
                operatorDTO.OperatorFirstName = o.OperatorFirstName is null ? "" : o.OperatorFirstName;
                operatorDTO.OperatorlastName = o.OperatorlastName is null ? "" : o.OperatorlastName;
                operatorDTO.OrgName = o.OrgName is null ? "" : o.OrgName;
                operatorDTO.AddressLine1 = o.AddressLine1;
                operatorDTO.AddressLine2 = o.AddressLine2 is null ? "" : o.AddressLine2;
                operatorDTO.Town = o.Town is null ? "" : o.Town;
                operatorDTO.County = o.County is null ? "" : o.County;
                operatorDTO.Postcode = o.Postcode;
                operatorDTO.Email = o.Email;
                operatorDTO.mobile = o.Mobile is null ? "Not Known" : o.Mobile;

                OperatorDetails.Add(operatorDTO);
            }
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });

            base.OnInitialized();
        }
        public async void GoToNextPage()
        {
            var value = _yesnoOption.YesNoOptions.ToString();

            bool forceLoad = false;

            if (value == "Yes")
            {
                NavigationManager.NavigateTo("/add-operator/", forceLoad);
            }
            else
            {
                NavigationManager.NavigateTo("/alternate-supervising-engineer", forceLoad);
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
            string pagelink = "/reservoir-details";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}