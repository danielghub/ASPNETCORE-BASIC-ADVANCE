using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddingAdditionalColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TIN",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("10a19a59-7510-4cf3-8bfb-e1e5b2a5869c"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("122fbaee-1dd0-47a3-8e1b-2c747115e98b"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("2a9e6725-d7aa-4e7c-8bb2-57340f482fef"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("318d659b-565f-453c-9d08-4bb54af761ac"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("386d21aa-a2fc-45ca-80fe-9b7b6bb00bd8"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("39bb902e-26b8-44e2-9f39-f40688d847e0"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("3ea39dec-8989-4263-81d3-28827503d113"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("54721a0b-8149-423a-ad9c-197ef3f027d9"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("80c0f9b5-7fae-4fec-89d8-6f8b43659e72"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("8efe97e3-8093-4ec1-96ae-7f31889e5db7"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("8f90c466-9ac4-4429-9fc8-247ff938695a"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("ed525570-c530-4268-8e17-68c077416da6"),
                column: "TIN",
                value: null);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("f1dfe911-2bdb-4e8b-977b-5f91c0411478"),
                column: "TIN",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TIN",
                table: "Persons");
        }
    }
}
