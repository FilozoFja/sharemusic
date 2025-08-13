using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharemusic.Migrations
{
    /// <inheritdoc />
    public partial class Tokenfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SpotifyTokens",
                table: "SpotifyTokens");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SpotifyTokens",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpotifyTokens",
                table: "SpotifyTokens",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SpotifyTokens",
                table: "SpotifyTokens");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SpotifyTokens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpotifyTokens",
                table: "SpotifyTokens",
                column: "AccessToken");
        }
    }
}
