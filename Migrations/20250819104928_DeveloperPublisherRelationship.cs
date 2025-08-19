using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoGameApi.Migrations
{
    /// <inheritdoc />
    public partial class DeveloperPublisherRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Developer",
                table: "VideoGames");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "VideoGames");

            migrationBuilder.AddColumn<string>(
                name: "DeveloperId",
                table: "VideoGames",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublisherId",
                table: "VideoGames",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Developer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "DeveloperId", "PublisherId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "DeveloperId", "PublisherId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "DeveloperId", "PublisherId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "DeveloperId", "PublisherId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "DeveloperId", "PublisherId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "6",
                columns: new[] { "DeveloperId", "PublisherId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "7",
                columns: new[] { "DeveloperId", "PublisherId" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_VideoGames_DeveloperId",
                table: "VideoGames",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoGames_PublisherId",
                table: "VideoGames",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoGames_Developer_DeveloperId",
                table: "VideoGames",
                column: "DeveloperId",
                principalTable: "Developer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoGames_Publisher_PublisherId",
                table: "VideoGames",
                column: "PublisherId",
                principalTable: "Publisher",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoGames_Developer_DeveloperId",
                table: "VideoGames");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoGames_Publisher_PublisherId",
                table: "VideoGames");

            migrationBuilder.DropTable(
                name: "Developer");

            migrationBuilder.DropTable(
                name: "Publisher");

            migrationBuilder.DropIndex(
                name: "IX_VideoGames_DeveloperId",
                table: "VideoGames");

            migrationBuilder.DropIndex(
                name: "IX_VideoGames_PublisherId",
                table: "VideoGames");

            migrationBuilder.DropColumn(
                name: "DeveloperId",
                table: "VideoGames");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "VideoGames");

            migrationBuilder.AddColumn<string>(
                name: "Developer",
                table: "VideoGames",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "VideoGames",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "Developer", "Publisher" },
                values: new object[] { "Nintendo EPD", "Nintendo" });

            migrationBuilder.UpdateData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "Developer", "Publisher" },
                values: new object[] { "Santa Monica Studio", "Sony Interactive Entertainment" });

            migrationBuilder.UpdateData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "Developer", "Publisher" },
                values: new object[] { "343 Industries", "Xbox Game Studios" });

            migrationBuilder.UpdateData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "Developer", "Publisher" },
                values: new object[] { "Nintendo EPD", "Nintendo" });

            migrationBuilder.UpdateData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "Developer", "Publisher" },
                values: new object[] { "CD Projekt Red", "CD Projekt" });

            migrationBuilder.UpdateData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "6",
                columns: new[] { "Developer", "Publisher" },
                values: new object[] { "Square Enix", "Square Enix" });

            migrationBuilder.UpdateData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: "7",
                columns: new[] { "Developer", "Publisher" },
                values: new object[] { "Nintendo EPD", "Nintendo" });
        }
    }
}
