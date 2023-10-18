using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RACE2.DataModel;
using RACE2.Dto;
using RACE2.FrontEndWebServer.FluxorImplementation.Actions;
using RACE2.FrontEndWebServer.FluxorImplementation.Stores;
using RACE2.Services;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class UserLockout
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        protected override async void OnInitialized()
        {
            await base.OnInitializedAsync();
        }        
    }
}
