using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirdApp.Models;

namespace BirdApp.Controllers
{
    public class AccountController : Controller
    {
        // fields for Identity
        private UserManager<BirdWatcher> userManager;
        private SignInManager<BirdWatcher> signInManager;

        // constructor to enable Identity
        public AccountController(UserManager<BirdWatcher> userMngr, SignInManager<BirdWatcher> signInMngr)
        {
            userManager = userMngr;
            signInManager = signInMngr;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM rVModel)
        {
            // check model for validation
            if (ModelState.IsValid)
            {
                // create Reviewer object using RegisterViewModel parameter, assign UserName
                var watcher = new BirdWatcher { UserName = rVModel.Username };
                // create result using UserManager
                var result = await userManager.CreateAsync(watcher, rVModel.Password);
                // if result is successfully created, sign in new reviewer and redirect to Home Index
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(watcher, isPersistent: true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // for each error in the result, add model error with description
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(rVModel);
        }
        [HttpGet]
        public IActionResult LogIn(string returnURL = "")
        {
            // create LoginViewModel object and initialize its ReturnURL property using the string parameter 
            var lVModel = new LoginVM { ReturnUrl = returnURL };
            return View(lVModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginVM lVModel)
        {
            // check model for validation
            if (ModelState.IsValid)
            {
                // create result via SignInManager
                var result = await signInManager.PasswordSignInAsync(
                lVModel.UserName, lVModel.Password, isPersistent: lVModel.RememberMe,
                lockoutOnFailure: false);
                // if result is successfully created and if ReturnURL is empty or local URL, redirect w/ReturnURL
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(lVModel.ReturnUrl) && Url.IsLocalUrl(lVModel.ReturnUrl))
                    {
                        return Redirect(lVModel.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid username/password");
            return View(lVModel);
        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            // sign out via SignInManager
            await signInManager.SignOutAsync();
            // redirect to Home Index
            return RedirectToAction("Index", "Home");
        }
        // Accessed Denied method
        public ViewResult AccessDenied()
        {
            return View();
        }
    }
}
