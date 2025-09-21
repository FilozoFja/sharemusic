using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharemusic.Migrations
{
    /// <inheritdoc />
    public partial class PlaylistLocalPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocalPhotoUrl",
                table: "Playlists",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalPhotoUrl",
                table: "Playlists");
        }
    }
}
