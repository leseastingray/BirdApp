using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirdApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Name = "California Scrub Jay";
            ViewBag.Location = "West of the Cascades";
            ViewBag.Length = 11.5;
            return View();
        }

        // Bird of the Day - Randomly choose a bird species from the collection and display here
    }
}
