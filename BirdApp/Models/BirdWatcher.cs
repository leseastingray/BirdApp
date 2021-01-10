using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BirdApp.Models
{
    public class BirdWatcher
    {
        public string WatcherLastName { get; set; }
        public string WatcherFirstName { get; set; }
        public int WatcherAge { get; set; }
        [Required(ErrorMessage = "Please enter a US state abbreviation.")]
        [StringLength(2)]
        public string WatcherState { get; set; }
        [Required(ErrorMessage = "Please enter an email.")]
        [EmailAddress]
        public string WatcherEmail { get; set; }
    }
}
