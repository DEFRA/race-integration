using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RACE2.FrontEnd.Pages.S12Pages
{
    public partial class S12StatementDraftOrSubmit
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public SignOutSessionStateManager SignOutManager { get; set; } = default!;

        private DraftSubmitClass draftsubmit = new DraftSubmitClass();
        private int Index = 1;
        public List<string> LabelsForButtons = new List<String> { "","Submit your statement", "Send a draft"};
        public void GoToNextPage()
        {
            var value = draftsubmit.DraftSubmitOptions.ToString();

            bool forceLoad = false;

            if (value == "SubmitYourStatement")
            {
                NavigationManager.NavigateTo("/s12-statement-confirmation/", forceLoad);
            }
            else if (value == "SendADraft")
            {
                NavigationManager.NavigateTo("/s12-statement-send-draft", forceLoad);
            }
        }

        private async Task BeginSignOut(MouseEventArgs args)
        {
            await SignOutManager.SetSignOutState();
            NavigationManager.NavigateTo("authentication/logout");
        }
        public class DraftSubmitClass
        {
            public enum DraftSubmitEnum {
                [Display(Name = "Send a draft")]
                SendADraft = 1,
                [Display(Name = "Submit your statement")]
                SubmitYourStatement = 2 
            };
            public DraftSubmitEnum DraftSubmitOptions = 0;            
        }
    }
}
