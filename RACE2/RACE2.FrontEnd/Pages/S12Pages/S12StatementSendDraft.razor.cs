using Microsoft.AspNetCore.Components;
using RACE2.FrontEnd.Components;
using RACE2.FrontEnd.Utilities;

namespace RACE2.FrontEnd.Pages.S12Pages
{
    public partial class S12StatementSendDraft
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        private DraftModel draftModel => new DraftModel();

        public async Task GoToNextPage()
        {
            bool forceLoad = false;
            NavigationManager.NavigateTo("/s12-statement-confirmation-draft-sent", forceLoad);
        }

        public class DraftModel
        {
            public string Email { get; set; }
        }
    }
}
