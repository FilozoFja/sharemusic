using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharemusic.Migrations
{
    /// <inheritdoc />
    public partial class AlbumDbModelFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Artists_ArtistSpotifyId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Albums");

            migrationBuilder.AlterColumn<string>(
                name: "CoverImageUrl",
                table: "Albums",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "ArtistSpotifyId",
                table: "Albums",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Artists_ArtistSpotifyId",
                table: "Albums",
                column: "ArtistSpotifyId",
                principalTable: "Artists",
                principalColumn: "SpotifyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Artists_ArtistSpotifyId",
                table: "Albums");

            migrationBuilder.AlterColumn<string>(
                name: "CoverImageUrl",
                table: "Albums",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ArtistSpotifyId",
                table: "Albums",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Albums",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Artists_ArtistSpotifyId",
                table: "Albums",
                column: "ArtistSpotifyId",
                principalTable: "Artists",
                principalColumn: "SpotifyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
