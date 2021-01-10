using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BirdApp.Models
{
    public class BirdSighting
    {
        // Bird in sighting
        [Required(ErrorMessage = "Please enter a bird name or type Unknown.")]
        [StringLength(60, MinimumLength = 3)]
        public string BirdName { get; set; }
        // Habitat in which bird was sighted
        [Required(ErrorMessage = "Please enter a habitat in which the bird was sighted.")]
        public string Habitat { get; set; }
        // Estimated bird length in inches
        [Required (ErrorMessage ="Please enter a length in inches.")]
        [Range(0.1, 60)]
        public float? Length { get; set; }
        // Description
        public string Description { get; set; }
        // Name of bird watcher who made the sighting
        [Required(ErrorMessage = "Please enter a name for the watcher who made the sighting.")]
        public string WatcherName { get; set; }
        [Required(ErrorMessage = "Please choose a date for the sighting.")]
        // Date of bird sighting
        public DateTime SightingDate { get; set; }

        // Timestamp for sighting post (p478?)
    }
}
