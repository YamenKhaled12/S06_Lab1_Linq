using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LinqEtSeedEF.Migrations
{
    /// <inheritdoc />
    public partial class AjoutCommande : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Commande",
                columns: new[] { "Id", "ClientId", "Date", "RestaurantId" },
                values: new object[] { 5, 2, new DateTime(2022, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.InsertData(
                table: "CommandePlat",
                columns: new[] { "CommandeId", "PlatId", "Quantite" },
                values: new object[,]
                {
                    { 5, 3, 3 },
                    { 5, 4, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CommandePlat",
                keyColumns: new[] { "CommandeId", "PlatId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "CommandePlat",
                keyColumns: new[] { "CommandeId", "PlatId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "Commande",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
