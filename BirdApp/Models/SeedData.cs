using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BirdApp.Models
{
    public class SeedData
    {
        public static void Seed(BirdAppContext context, UserManager<BirdWatcher> userManager,
                                          RoleManager<IdentityRole> roleManager)
        {
            if (!context.Species.Any())
            {
                // TODO: check the results and do something if the operation failed--if it ever does
                var result = roleManager.CreateAsync(new IdentityRole("Member")).Result;
                result = roleManager.CreateAsync(new IdentityRole("Admin")).Result;

                // Seeds a default administrator
                BirdWatcher siteAdmin = new BirdWatcher
                {
                    UserName = "ReviewAdmin",
                    WatcherFirstName = "Admin Person"
                };
                userManager.CreateAsync(siteAdmin, "africanPenguin1$").Wait();
                IdentityRole adminRole = roleManager.FindByNameAsync("Admin").Result;
                userManager.AddToRoleAsync(siteAdmin, adminRole.Name);

                // Seed users and reviews for manual site testing
                // The next multiple reviews will be by the same user, so I will create
                // the user object once and store it so that both reviews will be
                // associated with the same entity in the DB.
                BirdWatcher stingray = new BirdWatcher
                {
                    UserName = "stingray",
                    WatcherFirstName = "Amy",
                    WatcherLastName = "Lese",
                    WatcherAge = 30,
                    WatcherState = "OR"
                };
                context.Users.Add(stingray);
                context.SaveChanges();   // This will add a UserID to the Reviewer object

                BirdSpecies bird = new BirdSpecies
                {
                    Name = "California Scrub Jay",
                    ScientificName = "Aphelocoma californica",
                    Family = "Corvidae",
                    TypicalSize = 3,
                    PrimaryColor = "Blue",
                    Description = "Loud calling bird that tends to live in pairs or small family groups. Omnivore, but commonly found eating or caching nuts and acorns. Uses bowl-shaped nests in trees.",
                    Habitat = "Scrub",
                    BirdWatcher = stingray
                };
                context.Species.Add(bird);  // queues up the review to be added to the DB

                bird = new BirdSpecies
                {
                    Name = "Steller's Jay",
                    ScientificName = "Cyanocitta stelleri",
                    Family = "Corvidae",
                    TypicalSize = 3,
                    PrimaryColor = "Blue",
                    Description = "Loud calling bird with dark crest. Omnivore, but commonly found eating or caching nuts and acorns. Uses bowl-shaped nests in trees.",
                    Habitat = "Forest",
                    BirdWatcher = stingray
                };
                context.Species.Add(bird);

                bird = new BirdSpecies
                {
                    Name = "American Crow",
                    ScientificName = "Corvus brachyrhynchos",
                    Family = "Corvidae",
                    TypicalSize = 4,
                    PrimaryColor = "Black",
                    Description = "Loud, large adaptable bird that forms pairs but often found in flocks. Omnivore, including eggs and carrion.",
                    Habitat = "Open areas"
                };
                context.Species.Add(bird);
                BirdWatcher spot = new BirdWatcher
                {
                    UserName = "Spot",
                    WatcherFirstName = "Matt",
                    WatcherLastName = "Mulls",
                    WatcherAge = 45,
                    WatcherState = "CA"
                };
                context.Users.Add(spot);
                context.SaveChanges();   // This will add a UserID to the Reviewer object

                bird = new BirdSpecies
                {
                    Name = "Anna's Hummingbird",
                    ScientificName = "Calypte anna",
                    Family = "Trochilidae",
                    TypicalSize = 0,
                    PrimaryColor = "Green",
                    Description = "Tiny hovering bird, highly territorial. Males have throat feathers that appear red from some angles. High-pitched, loud clicking call.",
                    Habitat = "Open areas and scrub"
                };
                context.Species.Add(bird);

                BirdWatcher meganCat = new BirdWatcher()
                {
                    UserName = "Megan cat",
                    WatcherFirstName = "Megan",
                    WatcherLastName = "Smith",
                    WatcherAge = 25,
                    WatcherState = "CA"
                };
                context.Users.Add(meganCat);
                context.SaveChanges();   // This will add a UserID to the reviewer object
                
                bird = new BirdSpecies
                {
                    Name = "Dark-eyed Junco",
                    ScientificName = "Junco hyemalis",
                    Family = "Passerellidae",
                    TypicalSize = 0,
                    PrimaryColor = "Brown",
                    Description = "Typical sparrow with variable coloring. Can be dominately brown or grey, but most varieties have in common bright white tail feathers. Eats primarily seeds.",
                    Habitat = "Forest"
                };
                context.Species.Add(bird);
                // Stores all the seeded reviews in the database
                context.SaveChanges();
            }
        }
    }
}
