﻿using Fluxor;
using Microsoft.AspNetCore.Components;
using RACE2.Dto;
using RACE2.FrontEndWebServer.FluxorImplementation.Stores;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class NewReservoir
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IState<CurrentUserDetailState> State { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        public CurrentUserDetailState CurrentUserDetailState => State.Value;

        public ReservoirDetailsDTO newReservoir = new ReservoirDetailsDTO();
        public string Text { get; set; } = "????";
        public string ButtonText { get; set; } = "Click Me";
        public int ButtonClicked { get; set; }

        void ButtonOnClick()
        {
            ButtonClicked += 1;
            Text = $"Awesome x {ButtonClicked}";
            ButtonText = "Click Me Again";
        }
        protected override void OnInitialized()
        {
            //newReservoir = CurrentUserDetailState.ReservoirDetailsDTO;
            base.OnInitialized();
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/reservoir-not-listed";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
    }
}
