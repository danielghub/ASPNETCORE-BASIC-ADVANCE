using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiveNewsLetters = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "CountryName" },
                values: new object[,]
                {
                    { new Guid("12ac98a9-22cc-4fe3-a941-d666e70ce71d"), "Malaysia" },
                    { new Guid("1630c32a-8ec0-4a86-8f38-084e40bbcd8a"), "USA" },
                    { new Guid("1c2c8e94-f38a-4f99-96ad-61d53ef41db4"), "Taiwan" },
                    { new Guid("2a88ed58-3ea9-47b3-8451-e507439846b7"), "Thailand" },
                    { new Guid("2dbc0bbc-2de5-4678-9f77-07ae6fd49551"), "Indonesia" },
                    { new Guid("6a195203-30fb-405d-b730-ba70586cb31d"), "Ukraine" },
                    { new Guid("704955d7-5317-4a6c-8e88-cd130214494e"), "Columbia" },
                    { new Guid("7f433398-0260-45e4-928e-528755bf9fe6"), "Philippines" },
                    { new Guid("94e12b76-69ee-4a24-b08a-86c358b66404"), "China" },
                    { new Guid("ba17206b-4b13-454f-b9a4-af63de3be589"), "Singapore" },
                    { new Guid("ca4c39fe-085c-47a0-b365-bad2f48eb3a0"), "France" },
                    { new Guid("f310d3da-6ba2-48fb-ac40-3496dc26f076"), "Russia" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "Address", "CountryID", "DateOfBirth", "Email", "Gender", "PersonName", "ReceiveNewsLetters" },
                values: new object[,]
                {
                    { new Guid("10a19a59-7510-4cf3-8bfb-e1e5b2a5869c"), null, null, new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc), "cpoplandc@sitemeter.com", "Polygender", "Coop", false },
                    { new Guid("122fbaee-1dd0-47a3-8e1b-2c747115e98b"), "33231 Petterle Hill", null, new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc), "fedgeon0@newsvine.com", "Non-binary", "Ferdie", false },
                    { new Guid("2a9e6725-d7aa-4e7c-8bb2-57340f482fef"), "22939 Rusk Way", null, new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc), "frennebach5@homestead.com", "Male", "Freeman", true },
                    { new Guid("318d659b-565f-453c-9d08-4bb54af761ac"), "4888 Hermina Avenue", null, new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc), "akirley7@yolasite.com", "Female", "Ariel", false },
                    { new Guid("386d21aa-a2fc-45ca-80fe-9b7b6bb00bd8"), "4399 Dwight Way", null, new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc), "khenbury1@marketwatch.com", "Female", "Kathye", false },
                    { new Guid("39bb902e-26b8-44e2-9f39-f40688d847e0"), "98786 Talisman Place", null, new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc), "vdavers2@google.ca", "Female", "Vivian", true },
                    { new Guid("3ea39dec-8989-4263-81d3-28827503d113"), "29239 Hovde Plaza", null, new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc), "afurzea@creativecommons.org", "Agender", "Aliza", false },
                    { new Guid("54721a0b-8149-423a-ad9c-197ef3f027d9"), "625 Arkansas Hill", null, new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc), "mreynardb@cmu.edu", "Female", "Mirna", true },
                    { new Guid("80c0f9b5-7fae-4fec-89d8-6f8b43659e72"), "89526 Debra Hill", null, new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc), "crawstorn8@irs.gov", "Female", "Cristabel", true },
                    { new Guid("8efe97e3-8093-4ec1-96ae-7f31889e5db7"), "1 Golf View Way", null, new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc), "menticott4@amazon.de", "Female", "Mame", false },
                    { new Guid("8f90c466-9ac4-4429-9fc8-247ff938695a"), "2957 Ohio Place", null, new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc), "baberkirdo3@who.int", "Male", "Boot", false },
                    { new Guid("ed525570-c530-4268-8e17-68c077416da6"), "5 High Crossing Street", null, new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc), "ltrevear9@berkeley.edu", "Female", "Leanna", false },
                    { new Guid("f1dfe911-2bdb-4e8b-977b-5f91c0411478"), "2 Stone Corner Place", null, new DateTime(2023, 1, 15, 13, 57, 6, 0, DateTimeKind.Utc), "rjeffrey6@odnoklassniki.ru", "Female", "Rozanna", false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
