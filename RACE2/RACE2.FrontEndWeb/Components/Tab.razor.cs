﻿using Microsoft.AspNetCore.Components;

namespace RACE2.FrontEndWeb.Components
{
    public partial class Tab
    {
        [CascadingParameter]
        private Tabs Parent { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public bool Enabled { get; set; } = true;

        protected override void OnInitialized()
        {
            if (Parent == null)
                throw new ArgumentNullException(nameof(Parent), "TabPage must exist within a TabControl");

            base.OnInitialized();
            Parent.AddPage(this);
        }
    }
}
