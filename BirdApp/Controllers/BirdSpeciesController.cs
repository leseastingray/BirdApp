using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirdApp.Controllers
{
    public class BirdSpeciesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
