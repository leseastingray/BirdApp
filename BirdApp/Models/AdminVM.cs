using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BirdApp.Models
{
    public class AdminVM
    {
        // Holds collection of BirdWatcher objects
        public IEnumerable<BirdWatcher> Watchers { get; set; }
        // Holds collection of Role objects
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
