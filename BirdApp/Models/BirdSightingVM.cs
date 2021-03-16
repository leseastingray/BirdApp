using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BirdApp.Models
{
    public class BirdSightingVM
    {
        // This identifies the Bird to which the sighting is linked
        public int BirdID { get; set; }
        public string BirdName { get; set; }
        [Required(ErrorMessage = "Please enter a habitat in which the bird was sighted.")]
        public string Habitat { get; set; }
        [Range(0.1, 60)]
        public float? Length { get; set; }
        // Description text
        public string Description { get; set; }
        // Choosable date for bird sighting
        [Required(ErrorMessage = "Please choose a date for the sighting.")]
        public DateTime SightingDate { get; set; }
    }
}
