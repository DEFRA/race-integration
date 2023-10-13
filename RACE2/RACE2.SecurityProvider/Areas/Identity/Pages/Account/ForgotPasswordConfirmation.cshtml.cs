// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RACE2.DataModel;
using RACE2.Notification;
using Microsoft.AspNetCore.Mvc;

namespace RACE2.SecurityProvider.Areas.Identity.Pages.Account
{
    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [AllowAnonymous]
    public class ForgotPasswordConfirmation : PageModel
    {
        private readonly IConfiguration _config;
        public ForgotPasswordConfirmation(IConfiguration config)
        {
            _config = config;
        }
        public string WebAppUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public void OnGet()
        {
            WebAppUrl = _config["RACE2FrontEndURL"];
        }

        public IActionResult ResendEmail()
        {
            return RedirectToPage("./ForgotPassword");
        }
    }
}
