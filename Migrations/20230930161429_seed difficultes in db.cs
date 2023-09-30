using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalksAPI.Migrations
{
    /// <inheritdoc />
    public partial class seeddifficultesindb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("af5f372b-bad1-4b04-a30d-81f1e6952551"), "Medium" },
                    { new Guid("c08f7113-d920-4e59-b9ad-eb961c6d8fad"), "Easy" },
                    { new Guid("d86c36e2-bd3e-46b5-afba-f2bd295f61e9"), "Hard" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("af5f372b-bad1-4b04-a30d-81f1e6952551"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("c08f7113-d920-4e59-b9ad-eb961c6d8fad"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("d86c36e2-bd3e-46b5-afba-f2bd295f61e9"));
        }
    }
}
