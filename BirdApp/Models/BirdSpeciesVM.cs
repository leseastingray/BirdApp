using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BirdApp.Models
{
    public class BirdSpeciesVM
    {
        [Required(ErrorMessage = "The bird name is required.")]
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
        [Required(ErrorMessage = "A bird image is required.")]
        [Display(Name = "Bird image")]
        public IFormFile BirdSpeciesImage { get; set; }
    }
}
