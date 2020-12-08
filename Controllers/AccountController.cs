/*
 * Author: Domenic Catalano
 * Date: Monday, December 7, 2020
 * Program Name: BooksForYou (Lab5)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Added these imports
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BooksForYou.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;


namespace BooksForYou.Controllers

{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Injects these three services to take care of user, sign in and logging management
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger _logger;

        // Contructor for AccountController
        public AccountController(
                    UserManager<AppUser> userManager,
                    SignInManager<AppUser> signInManager,
                    ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }



        #region Helpers

        // Takes care of errors
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        // Returns to the HomeController index
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion

        // AllowAnanymous means user can navigate to this page without being authenticated
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // Posts the create account page
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // For login page, does not require authentication
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // Posts the login page to allow user to sign in
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return RedirectToLocal(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // Redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        // Posts the logout functionality to allow user to sign out
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        /**** COMMUNICATION ACTIVITY 5 ****/

        // Using tutorial from https://code-maze.com/password-reset-aspnet-core-identity/
        // With a few slight modifications

        // Forgot Password Implementation:

        // This set of code will display the password reset form
        [HttpGet]
        [AllowAnonymous]    // Allows users that have not logged in access
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // This set of code is the logic behind the reset password
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            // Checks if the model state is valid
            if(!ModelState.IsValid)
            {
                return View(forgotPasswordViewModel);
            }

            // Checks the input against the database to find the user by email
            var user = await _userManager.FindByEmailAsync(forgotPasswordViewModel.Email);

            // If no user is found, we will still show a confirmation for security purposes
            if(user == null)
            {
                RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // This line of code is where the password change happens. The empty string is for a token that would usual go along with an email
            // But because it is local host I kept the token blank so the reset will function
            var passwordReset = await _userManager.ResetPasswordAsync(user, " ", forgotPasswordViewModel.Password);

            // If the password reset did not succeed
            if(!passwordReset.Succeeded)
            {
                foreach (var error in passwordReset.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                // Return the view
                return View();
            }

            // If everything worked, return the confirmation view
            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        // This set of code will display a password change confirmation to the user
        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
    }
}
