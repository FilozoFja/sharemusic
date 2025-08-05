using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharemusic.Migrations
{
    /// <inheritdoc />
    public partial class minorchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SongId",
                table: "Playlists");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SongId",
                table: "Playlists",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
