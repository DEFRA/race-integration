using Microsoft.AspNetCore.Components;

namespace RACE2.FrontEnd.Pages.S12Pages
{
    public partial class Tabs
    {
        [Parameter] public string TextFilling1 { get; set; }
        [Parameter] public string TextFilling2 { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter] public EventCallback<Tab> OnTabChanged { get; set; }

        public Tab ActivePage { get; set; }
        List<Tab> Pages = new List<Tab>();

        internal void AddPage(Tab tabPage)
        {
            Pages.Add(tabPage);
            if (Pages.Count == 1)
                ActivePage = tabPage;

            StateHasChanged();
        }

        string GetTabCSS(Tab page)
        {
            if (!page.Enabled)
                return "tab-disabled";

            return page == ActivePage ? "tab-active" : "";
        }

        void ActivatePage(Tab page)
        {
            if (page.Enabled)
            {
                ActivePage = page;
                OnTabChanged.InvokeAsync(page);
            }
        }
    }
}
