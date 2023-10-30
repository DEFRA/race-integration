﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RACE2.DataModel;
using System.Security.Claims;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class StatNonStat
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

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
