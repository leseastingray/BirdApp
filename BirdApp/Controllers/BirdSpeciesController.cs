using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BirdApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BirdApp.Controllers
{
    public class BirdSpeciesController : Controller
    {
        private readonly BirdAppContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        // This will help connect Reviewer to Review (and Comment)
        private UserManager<BirdWatcher> userManager;
        // For repo
        //IReviewRepository repo;
        public BirdSpeciesController(BirdAppContext context, IWebHostEnvironment hostEnvironment, UserManager<BirdWatcher> user)
        {
            dbContext = context;
            webHostEnvironment = hostEnvironment;
            userManager = user;

        }
        // This should provide a list of bird species to scroll through
        public async Task<IActionResult> Index()
        {
            var birds = await dbContext.Birds.ToListAsync();
            return View(birds);
        }
        public IActionResult BirdsByName()
        {
            // Get all reviews in the database and order by bird name
            var birds = dbContext.Birds.Include(bird => bird.BirdWatcher).OrderBy(b => b.Name).ToList();
            return View("Index", birds);
        }
        public IActionResult BirdsBySize()
        {
            // Get all reviews in the database and order by typical size
            var birds = dbContext.Birds.Include(bird => bird.BirdWatcher).OrderBy(b => b.TypicalSize).ToList();
            return View("Index", birds);
        }
        public IActionResult BirdsByHabitat()
        {
            // Get all reviews in the database and order by typical size
            var birds = dbContext.Birds.Include(bird => bird.BirdWatcher).OrderBy(b => b.Habitat).ToList();
            return View("Index", birds);
        }
        public IActionResult FindBirdById()
        {
            // declare and instantiate id
            int id = 1;
            // find first review matching the review id
            var bird = dbContext.Birds.Where(b => b.BirdID == id).FirstOrDefault();
            return View(bird);
        }
        [Authorize]
        [HttpGet]
        public IActionResult AddBird()
        {
            return View();
        }
        // This method should allow one to add a Bird with a bird image file
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBird(BirdSpeciesVM model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                BirdSpecies bird = new BirdSpecies
                {
                    Name = model.Name,
                    ScientificName = model.ScientificName,
                    Family = model.Family,
                    TypicalSize = model.TypicalSize,
                    PrimaryColor = model.PrimaryColor,
                    Description = model.Description,
                    Habitat = model.Habitat,
                    BirdPicture = uniqueFileName
                };
                dbContext.Add(bird);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        // This method should allow a bird image file to be uploaded
        private string UploadedFile(BirdSpeciesVM model)
        {
            string uniqueFileName = null;

            if (model.BirdSpeciesImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.BirdSpeciesImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.BirdSpeciesImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        // Method to get edit review form, requires log in
        [Authorize]
        [HttpGet]
        public IActionResult EditBird(int id)
        {
            // Gets the Appropriate Bird page
            var bird = dbContext.Birds.Find(id);
            return View(bird);
        }
        [HttpPost]
        public IActionResult EditBird(BirdSpecies bird)
        {
            // Method to edit reviews, adds to the context/repo
            if (ModelState.IsValid)
            {
                dbContext.Birds.Update(bird);
                dbContext.SaveChanges();
                return RedirectToAction("Index", "BirdSpecies");
            }
            else
            {
                return View(bird);
            }
        }
        // Method to get delete review form, requires log in
        [Authorize]
        [HttpGet]
        public IActionResult DeleteBird(int id)
        {
            // Gets the appropriate Delete page by id
            var bird = dbContext.Birds.Find(id);
            return View(bird);
        }
        [HttpPost]
        public IActionResult DeleteBird(BirdSpecies bird)
        {
            // Method to delete reviews
            dbContext.Birds.Remove(bird);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}  
