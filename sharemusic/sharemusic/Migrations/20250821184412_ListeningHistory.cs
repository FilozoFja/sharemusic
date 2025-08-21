using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharemusic.Migrations
{
    /// <inheritdoc />
    public partial class ListeningHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ListeningHistoryModelId",
                table: "Genres",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ListeningHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SongSpotifyId = table.Column<string>(type: "TEXT", nullable: false),
                    PlaylistId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListeningHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListeningHistory_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListeningHistory_Songs_SongSpotifyId",
                        column: x => x.SongSpotifyId,
                        principalTable: "Songs",
                        principalColumn: "SpotifyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genres_ListeningHistoryModelId",
                table: "Genres",
                column: "ListeningHistoryModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ListeningHistory_PlaylistId",
                table: "ListeningHistory",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_ListeningHistory_SongSpotifyId",
                table: "ListeningHistory",
                column: "SongSpotifyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_ListeningHistory_ListeningHistoryModelId",
                table: "Genres",
                column: "ListeningHistoryModelId",
                principalTable: "ListeningHistory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_ListeningHistory_ListeningHistoryModelId",
                table: "Genres");

            migrationBuilder.DropTable(
                name: "ListeningHistory");

            migrationBuilder.DropIndex(
                name: "IX_Genres_ListeningHistoryModelId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "ListeningHistoryModelId",
                table: "Genres");
        }
    }
}
