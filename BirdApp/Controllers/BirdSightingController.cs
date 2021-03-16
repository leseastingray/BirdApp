using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirdApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BirdApp.Controllers
{
    public class BirdSightingController : Controller
    {
        BirdAppContext context;
        UserManager<BirdWatcher> userManager;

        public BirdSightingController(BirdAppContext ctxt, UserManager<BirdWatcher> wU)
        {
            context = ctxt;
            userManager = wU;
        }
        // This should provide a list of bird sightings to scroll through
        public async Task<IActionResult> Index()
        {
            var sightings = await context.Sightings.ToListAsync();
            return View(sightings);
        }
        // Get the bird sighting form
        [Authorize]
        [HttpGet]
        public IActionResult AddSighting(int birdId)
        {
            var sightingVM = new BirdSightingVM { BirdID = birdId };
            return View(sightingVM);
        }
        // Method to add new bird sighting (see page 477 for DB context-related info)
        [HttpPost]
        public RedirectToActionResult AddSighting(BirdSightingVM sightingVM)
        {
            // BirdSighting is the domain model
            var sighting = new BirdSighting { 
                BirdName = sightingVM.BirdName,
                Habitat = sightingVM.Habitat,
                Length = sightingVM.Length,
                Description = sightingVM.Description,
            };
            sighting.BirdWatcher = userManager.GetUserAsync(User).Result;
            sighting.SightingDate = DateTime.Now;

            // Retrieve the bird that this sighting is for
            var bird = (from b in context.Birds
                        where b.Name == sightingVM.BirdName 
                        select b).First<BirdSpecies>();

            // Store the review with the comment in the database
            bird.Sightings.Add(sighting);
            context.SaveChanges();

            return RedirectToAction("Index", "BirdSpecies");
        }
        // Method to get edit review form, requires log in
        [Authorize]
        [HttpGet]
        public IActionResult EditSighting(int id)
        {
            // Gets the Appropriate Bird page
            var sighting = context.Sightings.Find(id);
            return View(sighting);
        }
        [HttpPost]
        public IActionResult EditSighting(BirdSighting sighting)
        {
            // Method to edit reviews, adds to the context/repo
            if (ModelState.IsValid)
            {
                context.Sightings.Update(sighting);
                context.SaveChanges();
                return RedirectToAction("Index", "BirdSighting");
            }
            else
            {
                return View(sighting);
            }
        }
        // Method to get delete review form, requires log in
        [Authorize]
        [HttpGet]
        public IActionResult DeleteSighting(int id)
        {
            // Gets the appropriate Delete page by id
            var sighting = context.Sightings.Find(id);
            return View(sighting);
        }
        [HttpPost]
        public IActionResult DeleteSighting(BirdSighting sighting)
        {
            // Method to delete reviews
            context.Sightings.Remove(sighting);
            context.SaveChanges();
            return RedirectToAction("Index", "BirdSighting");
        }
    }
}
