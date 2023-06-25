﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Entities.Migrations
{
    [DbContext(typeof(PersonDBContext))]
    [Migration("20230620145702_InsertPerson_StoredProcedure")]
    partial class InsertPerson_StoredProcedure
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Country", b =>
                {
                    b.Property<Guid?>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CountryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryId");

                    b.ToTable("Countries", (string)null);

                    b.HasData(
                        new
                        {
                            CountryId = new Guid("2a88ed58-3ea9-47b3-8451-e507439846b7"),
                            CountryName = "Thailand"
                        },
                        new
                        {
                            CountryId = new Guid("2dbc0bbc-2de5-4678-9f77-07ae6fd49551"),
                            CountryName = "Indonesia"
                        },
                        new
                        {
                            CountryId = new Guid("7f433398-0260-45e4-928e-528755bf9fe6"),
                            CountryName = "Philippines"
                        },
                        new
                        {
                            CountryId = new Guid("ba17206b-4b13-454f-b9a4-af63de3be589"),
                            CountryName = "Singapore"
                        },
                        new
                        {
                            CountryId = new Guid("94e12b76-69ee-4a24-b08a-86c358b66404"),
                            CountryName = "China"
                        },
                        new
                        {
                            CountryId = new Guid("1c2c8e94-f38a-4f99-96ad-61d53ef41db4"),
                            CountryName = "Taiwan"
                        },
                        new
                        {
                            CountryId = new Guid("12ac98a9-22cc-4fe3-a941-d666e70ce71d"),
                            CountryName = "Malaysia"
                        },
                        new
                        {
                            CountryId = new Guid("704955d7-5317-4a6c-8e88-cd130214494e"),
                            CountryName = "Columbia"
                        },
                        new
                        {
                            CountryId = new Guid("1630c32a-8ec0-4a86-8f38-084e40bbcd8a"),
                            CountryName = "USA"
                        },
                        new
                        {
                            CountryId = new Guid("ca4c39fe-085c-47a0-b365-bad2f48eb3a0"),
                            CountryName = "France"
                        },
                        new
                        {
                            CountryId = new Guid("f310d3da-6ba2-48fb-ac40-3496dc26f076"),
                            CountryName = "Russia"
                        },
                        new
                        {
                            CountryId = new Guid("6a195203-30fb-405d-b730-ba70586cb31d"),
                            CountryName = "Ukraine"
                        });
                });

            modelBuilder.Entity("Entities.Person", b =>
                {
                    b.Property<Guid>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CountryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ReceiveNewsLetters")
                        .HasColumnType("bit");

                    b.HasKey("PersonId");

                    b.ToTable("Persons", (string)null);

                    b.HasData(
                        new
                        {
                            PersonId = new Guid("122fbaee-1dd0-47a3-8e1b-2c747115e98b"),
                            Address = "33231 Petterle Hill",
                            DateOfBirth = new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc),
                            Email = "fedgeon0@newsvine.com",
                            Gender = "Non-binary",
                            PersonName = "Ferdie",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("386d21aa-a2fc-45ca-80fe-9b7b6bb00bd8"),
                            Address = "4399 Dwight Way",
                            DateOfBirth = new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc),
                            Email = "khenbury1@marketwatch.com",
                            Gender = "Female",
                            PersonName = "Kathye",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("39bb902e-26b8-44e2-9f39-f40688d847e0"),
                            Address = "98786 Talisman Place",
                            DateOfBirth = new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc),
                            Email = "vdavers2@google.ca",
                            Gender = "Female",
                            PersonName = "Vivian",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonId = new Guid("8f90c466-9ac4-4429-9fc8-247ff938695a"),
                            Address = "2957 Ohio Place",
                            DateOfBirth = new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc),
                            Email = "baberkirdo3@who.int",
                            Gender = "Male",
                            PersonName = "Boot",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("8efe97e3-8093-4ec1-96ae-7f31889e5db7"),
                            Address = "1 Golf View Way",
                            DateOfBirth = new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc),
                            Email = "menticott4@amazon.de",
                            Gender = "Female",
                            PersonName = "Mame",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("2a9e6725-d7aa-4e7c-8bb2-57340f482fef"),
                            Address = "22939 Rusk Way",
                            DateOfBirth = new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc),
                            Email = "frennebach5@homestead.com",
                            Gender = "Male",
                            PersonName = "Freeman",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonId = new Guid("f1dfe911-2bdb-4e8b-977b-5f91c0411478"),
                            Address = "2 Stone Corner Place",
                            DateOfBirth = new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc),
                            Email = "rjeffrey6@odnoklassniki.ru",
                            Gender = "Female",
                            PersonName = "Rozanna",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("318d659b-565f-453c-9d08-4bb54af761ac"),
                            Address = "4888 Hermina Avenue",
                            DateOfBirth = new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc),
                            Email = "akirley7@yolasite.com",
                            Gender = "Female",
                            PersonName = "Ariel",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("80c0f9b5-7fae-4fec-89d8-6f8b43659e72"),
                            Address = "89526 Debra Hill",
                            DateOfBirth = new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc),
                            Email = "crawstorn8@irs.gov",
                            Gender = "Female",
                            PersonName = "Cristabel",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonId = new Guid("ed525570-c530-4268-8e17-68c077416da6"),
                            Address = "5 High Crossing Street",
                            DateOfBirth = new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc),
                            Email = "ltrevear9@berkeley.edu",
                            Gender = "Female",
                            PersonName = "Leanna",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("3ea39dec-8989-4263-81d3-28827503d113"),
                            Address = "29239 Hovde Plaza",
                            DateOfBirth = new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc),
                            Email = "afurzea@creativecommons.org",
                            Gender = "Agender",
                            PersonName = "Aliza",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonId = new Guid("54721a0b-8149-423a-ad9c-197ef3f027d9"),
                            Address = "625 Arkansas Hill",
                            DateOfBirth = new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc),
                            Email = "mreynardb@cmu.edu",
                            Gender = "Female",
                            PersonName = "Mirna",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonId = new Guid("10a19a59-7510-4cf3-8bfb-e1e5b2a5869c"),
                            DateOfBirth = new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc),
                            Email = "cpoplandc@sitemeter.com",
                            Gender = "Polygender",
                            PersonName = "Coop",
                            ReceiveNewsLetters = false
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
