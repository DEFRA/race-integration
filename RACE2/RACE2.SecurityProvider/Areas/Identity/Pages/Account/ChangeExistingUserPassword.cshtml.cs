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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using RACE2.DataModel;

namespace RACE2.SecurityProvider.Areas.Identity.Pages.Account
{
    public class ChangeExistingUserPassword : PageModel
    {
        private readonly UserManager<UserDetail> _userManager;
        private readonly SignInManager<UserDetail> _signInManager;
        private readonly ILogger<ChangeYourPasswordModel> _logger;
        private readonly IConfiguration _config;

        public ChangeExistingUserPassword(
            UserManager<UserDetail> userManager,
            SignInManager<UserDetail> signInManager,
            ILogger<ChangeYourPasswordModel> logger,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _config = config;
            WebAppUrl = _config["RACE2FrontEndURL"];
        }

        [BindProperty]
        public string WebAppUrl { get; set; }

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
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Check user email")]
            //[EmailAddress(ErrorMessage = "Check your email format")]
            [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Check user email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Check user new password")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            WebAppUrl = _config["RACE2FrontEndURL"];
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("../Manage/SetPassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            WebAppUrl = _config["RACE2FrontEndURL"];

            if (!ModelState.IsValid)
            {
                return Page();
            }            

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                return NotFound($"Unable to load user with Email '{Input.Email}'.");
            }
            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                await _userManager.RemovePasswordAsync(user);
            }
            var changePasswordResult = await _userManager.AddPasswordAsync(user, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("The user password changed successfully.");
            StatusMessage = "User password has been changed.";

            string returnUrl = WebAppUrl + "/confirm-create-password";
            return Redirect(returnUrl);
        }
    }
}