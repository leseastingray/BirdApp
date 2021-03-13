using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirdApp.Models
{
    public class BirdAppContext : IdentityDbContext<BirdWatcher>
    {
        public BirdAppContext(DbContextOptions<BirdAppContext> options) : base(options) { }
        // DbSet for the database
        public DbSet<BirdSpecies> Birds { get; set; }
        public DbSet<BirdSighting> Sightings { get; set; }
        // This DbSet is now taken care of by the parent class, IdentityUser?
        //public List<BirdWatcher> BirdWatchers { get; set; }

        // New, now with comments! for extended domain model
        public DbSet<Comment> Comments { get; set; }
    }
}
