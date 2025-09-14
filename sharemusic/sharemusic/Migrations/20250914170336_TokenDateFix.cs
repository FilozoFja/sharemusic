using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharemusic.Migrations
{
    /// <inheritdoc />
    public partial class TokenDateFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiresIn",
                table: "SpotifyTokens");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedAt",
                table: "SpotifyTokens",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedAt",
                table: "SpotifyTokens");

            migrationBuilder.AddColumn<int>(
                name: "ExpiresIn",
                table: "SpotifyTokens",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
