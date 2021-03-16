using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BirdApp.Models
{
    public class BirdSighting
    {
        // ID for EF Core primary key
        [Key]
        public int SightingID { get; set; }

        // Declare and instantiate List field to hold associated Comment objects
        // When this is static, then the comments actually appear, but the same on every row...wat?
        private List<BirdSighting> sightings = new List<BirdSighting>();
        // Refers to BirdSpecies object
        public BirdSpecies Name { get; set; }
        public string BirdName { get; set; }
        // Habitat in which bird was sighted
        [Required(ErrorMessage = "Please enter a habitat in which the bird was sighted.")]
        public string Habitat { get; set; }

        // Estimated bird length in inches, range from 0.1 inch to 60 inches; not required
        [Range(0.1, 60)]
        public float? Length { get; set; }
        // Description text
        public string Description { get; set; }
        // Bird watcher who made the sighting; each Sighting can only be associated with one BirdWatcher
        public BirdWatcher BirdWatcher { get; set; }

        // Choosable date for bird sighting
        [Required(ErrorMessage = "Please choose a date for the sighting.")]
        public DateTime SightingDate { get; set; }

        // Get List holding associated BirdSighting objects
        public List<BirdSighting> Sightings
        {
            get { return sightings; }
        }
    }
}
