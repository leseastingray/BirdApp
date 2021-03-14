using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BirdApp.Models;

namespace BirdApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // initializes private UserManager and RoleManager objects for Controller access
        private UserManager<BirdWatcher> userManager;
        private RoleManager<IdentityRole> roleManager;

        // For eventual Repo hook up, initializes repo to pass into Controller
        //private IReviewRepository reviewRepo;

        // Constructor, takes UserManager, RoleManager (and eventually IReview Repo) as parameters
        public AdminController(UserManager<BirdWatcher> userMngr, RoleManager<IdentityRole> roleMngr)
        {
            userManager = userMngr;
            roleManager = roleMngr;
            // Add this assignment (and corresponding parameter) when adding Repos
            //reviewRepo = repo;
        }
        // Index method gets list of Reviewer objects and loops through, gets list of Roles
        public async Task<IActionResult> Index()
        {
            // Create new list of Reviewer objects
            List<BirdWatcher> watchers = new List<BirdWatcher>();

            // for each Reviewer in the UserManager's Users
            foreach (BirdWatcher watcher in userManager.Users)
            {
                // Gets roles and puts into RoleNames list<string> property of each reviewer
                watcher.RoleNames = await userManager.GetRolesAsync(watcher);
                // Add the reviewer and its associated list of RoleNames to reviewers list
                watchers.Add(watcher);
            }
            // Creates AdminViewModel and assigns reviewers list to Reviewers property, 
            //  Roles from rolemanager to Roles property
            AdminVM adminVM = new AdminVM
            {
                Watchers = watchers,
                Roles = roleManager.Roles
            };
            return View(adminVM);
        }
        // Delete action method
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            // instantiate reviewer object using id to find from UserManager
            BirdWatcher watcher = await userManager.FindByIdAsync(id);

            // if reviewer with the id exists, delete the reviewer
            if (watcher != null)
            {
                // try to delete reviewer from UserManager
                IdentityResult result = await userManager.DeleteAsync(watcher);
                // if result does not succeed, return error message listing each error
                if (!result.Succeeded)
                {
                    // initiate string to hold error message
                    string errorMessage = "";

                    // for each Identity Error in result's error, concatenate error's description
                    //  into error message string and return message as TempData
                    foreach (IdentityError error in result.Errors)
                    {
                        errorMessage += error.Description + " | ";
                    }
                    TempData["message"] = errorMessage;
                }
            }
            return RedirectToAction("Index");
        }
        // Add reviewer to Admin method, using reviewer's id as parameter
        [HttpPost]
        public async Task<IActionResult> AddToAdmin(string id)
        {
            // instantiate admin role object using RoleManager to find role with name "Admin"
            IdentityRole adminRole = await roleManager.FindByNameAsync("Admin");
            // if there no role called Admin exists, return error message via TempData
            if (adminRole == null)
            {
                TempData["message"] = "Admin role does not exist. " + "Click 'Create Admin Role'" +
                    "button to create it.";
            }
            else
            {
                //instantiate reviewer object using id to get it from UserManager and link it to Admin Role
                BirdWatcher watcher = await userManager.FindByIdAsync(id);
                await userManager.AddToRoleAsync(watcher, adminRole.Name);
            }
            return RedirectToAction("Index");
        }
        // Remove reviewer from Admin method, using reviewer's id as parameter
        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(string id)
        {
            // Instantiate reviewer object using id from UserManager and remove from Admin role
            BirdWatcher watcher = await userManager.FindByIdAsync(id);
            await userManager.RemoveFromRoleAsync(watcher, "Admin");
            return RedirectToAction("Index");
        }
        // Delete role method, using role's id as parameter
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            // Instantiate Identity role using id from RoleManager and remove role
            IdentityRole role = await roleManager.FindByIdAsync(id);
            await roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
        // Create Admin role method
        [HttpPost]
        public async Task<IActionResult> CreateAdminRole()
        {
            //
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            return RedirectToAction("Index");
        }
    }
}
