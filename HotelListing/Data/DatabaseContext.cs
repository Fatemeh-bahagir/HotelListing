using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Data
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Country>().HasData(
            new Country
            {
                Id = 1,
                Name = "Iran",
                ShortName = "IR"
            },
            new Country
            {
                Id = 2,
                Name = "Canada",
                ShortName = "CA"
            },
            new Country
            {
                Id = 3,
                Name = "United State",
                ShortName = "US"
            },
            new Country
            {
                Id = 4,
                Name = "United Kingdom",
                ShortName = "UK"
            }
            );

            builder.Entity<Hotel>().HasData(
            new Hotel
            {
                Id = 1,
                Name = "esteghlal",
                Address = "iran tehran",
                CountryId = 1,
                Rating = 4.5
            },
            new Hotel
            {
                Id = 2,
                Name = "amitis ",
                Address = "canada",
                CountryId = 2,
                Rating = 3.5
            },
            new Hotel
            {
                Id = 3,
                Name = "Barbara",
                Address = "united state america",
                CountryId = 3,
                Rating = 4.5
            },
            new Hotel
            {
                Id = 4,
                Name = "Barbara",
                Address = "united kingdom ",
                CountryId = 3,
                Rating = 4.5
            }
            );
        }

    }
}
