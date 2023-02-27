using Microsoft.AspNetCore.Components;

namespace RACE2.FrontEndWeb.Components
{
    public partial class StatNonStat
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        private StatNonStatClass _statnonstat = new StatNonStatClass();

        public void GoToNextPage()
        {
            var value = _statnonstat.StatNonStatOptions.ToString();

            bool forceLoad = true;

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
