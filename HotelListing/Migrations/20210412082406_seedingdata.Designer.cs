﻿// <auto-generated />
using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelListing.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210412082406_seedingdata")]
    partial class seedingdata
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HotelListing.Data.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Iran",
                            ShortName = "IR"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Canada",
                            ShortName = "CA"
                        },
                        new
                        {
                            Id = 3,
                            Name = "United State",
                            ShortName = "US"
                        },
                        new
                        {
                            Id = 4,
                            Name = "United Kingdom",
                            ShortName = "UK"
                        });
                });

            modelBuilder.Entity("HotelListing.Data.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "iran tehran",
                            CountryId = 1,
                            Name = "esteghlal",
                            Rating = 4.5
                        },
                        new
                        {
                            Id = 2,
                            Address = "canada",
                            CountryId = 2,
                            Name = "amitis ",
                            Rating = 3.5
                        },
                        new
                        {
                            Id = 3,
                            Address = "united state america",
                            CountryId = 3,
                            Name = "Barbara",
                            Rating = 4.5
                        },
                        new
                        {
                            Id = 4,
                            Address = "united kingdom ",
                            CountryId = 3,
                            Name = "Barbara",
                            Rating = 4.5
                        });
                });

            modelBuilder.Entity("HotelListing.Data.Hotel", b =>
                {
                    b.HasOne("HotelListing.Data.Country", "country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("country");
                });
#pragma warning restore 612, 618
        }
    }
}
