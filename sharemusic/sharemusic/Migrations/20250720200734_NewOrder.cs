using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharemusic.Migrations
{
    /// <inheritdoc />
    public partial class NewOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SongUrl",
                table: "Songs",
                newName: "SpotifyId");

            migrationBuilder.RenameColumn(
                name: "CoverImageUrl",
                table: "Songs",
                newName: "LocalSongPath");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Songs",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsExplicit",
                table: "Songs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LocalCoverPath",
                table: "Songs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Popularity",
                table: "Songs",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Songs",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "SpotifyTokens",
                columns: table => new
                {
                    AccessToken = table.Column<string>(type: "TEXT", nullable: false),
                    ExpiresIn = table.Column<int>(type: "INTEGER", nullable: false),
                    TokenType = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: true),
                    Scope = table.Column<string>(type: "TEXT", nullable: true),
                    RefreshToken = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpotifyTokens", x => x.AccessToken);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpotifyTokens");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "IsExplicit",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "LocalCoverPath",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "SpotifyId",
                table: "Songs",
                newName: "SongUrl");

            migrationBuilder.RenameColumn(
                name: "LocalSongPath",
                table: "Songs",
                newName: "CoverImageUrl");
        }
    }
}
