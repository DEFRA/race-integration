﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using RACE2.DataModel;

namespace RACE2.SecurityProvider.Areas.Identity.Pages.Account
{
    public class CreateNewPasswordModel : PageModel
    {
        private readonly UserManager<UserDetail> _userManager;
        private readonly IConfiguration _config;
        public CreateNewPasswordModel(UserManager<UserDetail> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            public string Email { get; set; }
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Check your password")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        [BindProperty]
        public string WebAppUrl { get; set; }

        public IActionResult OnGet(string userEmail)
        {
            WebAppUrl = _config["RACE2FrontEndURL"];
            Input =new InputModel { Email = userEmail };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            WebAppUrl = _config["RACE2FrontEndURL"];

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                string returnUrl = _config["RACE2FrontEndURL"] + "/login";
                return Redirect(returnUrl);
            }
            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                await _userManager.RemovePasswordAsync(user);
            }
            var result = await _userManager.AddPasswordAsync(user, Input.Password);
            if (result.Succeeded)
            {
                string returnUrl = _config["RACE2FrontEndURL"] + "/confirm-create-password";
                return Redirect(returnUrl);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
