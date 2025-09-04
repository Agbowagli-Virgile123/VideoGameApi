using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VideoGameApi.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Developer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    HashedPassword = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoGames",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Platform = table.Column<string>(type: "text", nullable: true),
                    DeveloperId = table.Column<string>(type: "text", nullable: true),
                    PublisherId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoGames_Developer_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Developer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VideoGames_Publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publisher",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GenreVideoGame",
                columns: table => new
                {
                    GenresId = table.Column<string>(type: "text", nullable: false),
                    VideoGamesId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreVideoGame", x => new { x.GenresId, x.VideoGamesId });
                    table.ForeignKey(
                        name: "FK_GenreVideoGame_Genre_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreVideoGame_VideoGames_VideoGamesId",
                        column: x => x.VideoGamesId,
                        principalTable: "VideoGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoGameDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VideoGameId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGameDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoGameDetails_VideoGames_VideoGameId",
                        column: x => x.VideoGameId,
                        principalTable: "VideoGames",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "VideoGames",
                columns: new[] { "Id", "DeveloperId", "Platform", "PublisherId", "Title" },
                values: new object[,]
                {
                    { "1", null, "Nintendo Switch", null, "The Legend of Zelda: Breath of the Wild" },
                    { "2", null, "PlayStation 4", null, "God of War" },
                    { "3", null, "Xbox Series X/S", null, "Halo Infinite" },
                    { "4", null, "Nintendo Switch", null, "Super Mario Odyssey" },
                    { "5", null, "PC, PlayStation 4, Xbox One, Nintendo Switch", null, "The Witcher 3: Wild Hunt" },
                    { "6", null, "PlayStation 4", null, "Final Fantasy VII Remake" },
                    { "7", null, "Nintendo Switch", null, "Animal Crossing: New Horizons" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenreVideoGame_VideoGamesId",
                table: "GenreVideoGame",
                column: "VideoGamesId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoGameDetails_VideoGameId",
                table: "VideoGameDetails",
                column: "VideoGameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VideoGames_DeveloperId",
                table: "VideoGames",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoGames_PublisherId",
                table: "VideoGames",
                column: "PublisherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreVideoGame");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "VideoGameDetails");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "VideoGames");

            migrationBuilder.DropTable(
                name: "Developer");

            migrationBuilder.DropTable(
                name: "Publisher");
        }
    }
}
