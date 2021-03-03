using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BirdApp.Models
{
    public class BirdSpecies
    {
        // ID for EF Core primary key
        [Key]
        public int BirdID { get; set; }

        // Declare and instantiate List field to hold associated Comment objects
        // When this is static, then the comments actually appear, but the same on every row...wat?
        private List<BirdSighting> sightings = new List<BirdSighting>();

        // Getters and Setters
        [Required(ErrorMessage = "The bird name is required.")]
        [StringLength(200, MinimumLength = 3,
            ErrorMessage = "Bird name must be between 3 and 200 characters.")]
        public string Name { get; set; }
        public string ScientificName { get; set; }
        public string Family { get; set; }

        [Required(ErrorMessage = "The typical size of the bird is required.")]
        [Range(0, 6, ErrorMessage = "Select a bird size.")]
        // 0 = sparrow-sized, 1 = small, 2 = robin-sized, 3 = medium, 4 = crow-sized,  5 = large, 6 = goose-sized or larger
        public int? TypicalSize { get; set; }
        [Required(ErrorMessage = "The primary color of the bird is required.")]
        public string PrimaryColor { get; set; }
        public string Description { get; set; }
        public string Habitat { get; set; }

        public BirdWatcher BirdWatcher { get; set; }

        // Get List holding associated Comment objects
        public List<BirdSighting> Sightings
        {
            get { return sightings; }
        }
    }
}
