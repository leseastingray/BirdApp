using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirdApp.Models
{
    public class BirdSighting
    {
        // Bird in sighting
        public string SightingBird { get; set; }
        // Name of bird watcher who made the sighting
        public string SightingWatcher { get; set; }
        // Date of bird sighting
        public DateTime SightingDate { get; set; }

        // Timestamp for sighting post (p478?)
    }
}
