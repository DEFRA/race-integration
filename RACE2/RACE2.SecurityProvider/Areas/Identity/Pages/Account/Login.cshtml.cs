// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RACE2.DataModel;
using IdentityServer4.Models;

namespace RACE2.SecurityProvider.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<UserDetail> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IConfiguration _config;

        public LoginModel(SignInManager<UserDetail> signInManager, ILogger<LoginModel> logger,IConfiguration config)
        {
            _signInManager = signInManager;
            _logger = logger;
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
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        [BindProperty]
        public string WebAppUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

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
            [Required(ErrorMessage = "Check your email address")]
            //[EmailAddress(ErrorMessage = "Check your email format")]
            [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",ErrorMessage = "Check your email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Check your password")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null) 
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
            
            returnUrl ??= Url.Content("~/");
            WebAppUrl = _config["RACE2FrontEndURL"];
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        [ValidateAntiForgeryToken]
        //[IgnoreAntiforgeryToken]
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            try
            {                
                returnUrl ??= Url.Content("~/");
                WebAppUrl = _config["RACE2FrontEndURL"];

                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

                if (ModelState.IsValid)
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User logged in." + " : " + Input.Email);
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        _logger.LogInformation("User logged in failed." + " : " + Input.Email);
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning("User account locked out." + " : " + Input.Email);
                        //var forgotPassLink = Url.Action(nameof(ForgotPassword), "Account", new { }, Request.Scheme);
                        //var content = string.Format("Your account is locked out, to reset your password, please click this link: {0}", forgotPassLink);
                        //var message = new Message(new string[] { userModel.Email }, "Locked out account information", content, null);
                        //await _emailSender.SendEmailAsync(message);
                        ModelState.AddModelError("", "The account is locked out");
                        //return RedirectToPage("./Lockout");
                        string returnUrl1 = WebAppUrl + "/user-lockout";
                        return Redirect(returnUrl1);
                    }
                    else
                    {
                        _logger.LogError("The email address or password you entered is incorrect." + " : " + Input.Email);
                        //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        ModelState.AddModelError(string.Empty, "The email address or password you entered is incorrect.");
                        return Page();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("System error!" + " : " + ex.Message);
                return Redirect("/error");
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
