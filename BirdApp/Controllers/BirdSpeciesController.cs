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

namespace BirdApp.Controllers
{
    public class BirdSpeciesController : Controller
    {
        private readonly BirdAppContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public BirdSpeciesController(BirdAppContext context, IWebHostEnvironment hostEnvironment)
        {
            dbContext = context;
            webHostEnvironment = hostEnvironment;
        }
        // This should provide a list of bird species to scroll through
        public async Task<IActionResult> Index()
        {
            var birds = await dbContext.Birds.ToListAsync();
            return View(birds);
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
    }
}  
