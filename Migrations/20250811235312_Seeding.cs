using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VideoGameApi.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VideoGames",
                columns: new[] { "Id", "Developer", "Platform", "Publisher", "Title" },
                values: new object[,]
                {
                    { "1", "Nintendo EPD", "Nintendo Switch", "Nintendo", "The Legend of Zelda: Breath of the Wild" },
                    { "2", "Santa Monica Studio", "PlayStation 4", "Sony Interactive Entertainment", "God of War" },
                    { "3", "343 Industries", "Xbox Series X/S", "Xbox Game Studios", "Halo Infinite" },
                    { "4", "Nintendo EPD", "Nintendo Switch", "Nintendo", "Super Mario Odyssey" },
                    { "5", "CD Projekt Red", "PC, PlayStation 4, Xbox One, Nintendo Switch", "CD Projekt", "The Witcher 3: Wild Hunt" },
                    { "6", "Square Enix", "PlayStation 4", "Square Enix", "Final Fantasy VII Remake" },
                    { "7", "Nintendo EPD", "Nintendo Switch", "Nintendo", "Animal Crossing: New Horizons" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "6");

            migrationBuilder.DeleteData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "7");
        }
    }
}
