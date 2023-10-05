using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Web;
using RACE2.Dto;
using Microsoft.AspNetCore.Components.Forms;
using RACE2.Services;
using System.IO;
using System.IO.Pipes;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Azure;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class CookiesStatement
    {
        protected bool IsLoggedIn { get; set; } = false;

        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (authState.User.Claims.Count() == 0)
                IsLoggedIn = false;
            else
                IsLoggedIn = true;
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
            base.OnInitializedAsync();
        }
    }
}
