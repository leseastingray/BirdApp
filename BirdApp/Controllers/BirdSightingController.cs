using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirdApp.Models;

namespace BirdApp.Controllers
{
    public class BirdSightingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        // Method to add new bird sighting (see page 477 for DB context-related info)
        [HttpPost]
        public IActionResult AddSighting(BirdSighting sighting)
        {
            // Validate model
            /* if (ModelState.IsValid)
            {

            } */
            return RedirectToAction("List", "Sighting");
        }
        // Method to update bird sighting
        // Method to delete bird sighting
    }
}
