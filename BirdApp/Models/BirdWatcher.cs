using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BirdApp.Models
{
    public class BirdWatcher : IdentityUser
    {
        // This Property is now included in the inherited IdentityUser class
        /* ID for EF Core primary key
         * public int BirdWatcherID { get; set; }
         */
        public string WatcherLastName { get; set; }
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Name must contain 3 to 60 characters.")]
        public string WatcherFirstName { get; set; }
        public int WatcherAge { get; set; }
        //[Required(ErrorMessage = "Please enter a US state abbreviation.")]
        [StringLength(2)]
        public string WatcherState { get; set; }

        // Not mapped means EF excludes this from the database
        // This property allows for an easy way to display a list of the reviwer's roles
        [NotMapped]
        public IList<string> RoleNames { get; set; }
    }
}
