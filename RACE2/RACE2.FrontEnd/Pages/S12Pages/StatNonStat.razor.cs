using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using RACE2.FrontEnd.FluxorImplementation.Stores;
using RACE2.FrontEnd.RACE2GraphQLSchema;
using System.Security.Claims;
using RACE2.FrontEnd.FluxorImplementation.Actions;

namespace RACE2.FrontEnd.Pages.S12Pages
{
    public partial class StatNonStat
    {
        [Inject]
        public RACE2GraphQLClient client { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IState<CurrentUserDetailState> CurrentUserDetailState { get; set; } = default!;
        [Inject]
        public IState<CurrentReservoirState> CurrentReservoirState { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;
        private StatNonStatClass _statnonstat = new StatNonStatClass();

        public void GoToNextPage()
        {
            var value = _statnonstat.StatNonStatOptions.ToString();

            bool forceLoad = false;

            if (value == "Statutory")
            {
                NavigationManager.NavigateTo("/choose-a-reservoir/", forceLoad);
            }
            else
            {
                NavigationManager.NavigateTo("/non-stat", forceLoad);
            }
        }
    }

    public class StatNonStatClass
    {
        public enum StatNonStatEnum { Statutory = 1, NonStatutory = 2 };
        public StatNonStatEnum StatNonStatOptions = 0;
    }
}
