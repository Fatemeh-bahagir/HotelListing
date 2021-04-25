﻿using HotelListing.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>

    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
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
