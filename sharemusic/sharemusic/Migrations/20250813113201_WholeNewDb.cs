using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharemusic.Migrations
{
    /// <inheritdoc />
    public partial class WholeNewDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExplicit",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "Popularity",
                table: "Songs",
                newName: "SongLengthInSeconds");

            migrationBuilder.RenameColumn(
                name: "LocalCoverPath",
                table: "Songs",
                newName: "ReleaseDate");

            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Songs",
                newName: "ArtistModelId");

            migrationBuilder.AlterColumn<int>(
                name: "PlaylistModelId",
                table: "Songs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArtistId",
                table: "Songs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SpotifyId",
                table: "Playlists",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Playlists",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    SpotifyId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Genres = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Songs_ArtistModelId",
                table: "Songs",
                column: "ArtistModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Artists_ArtistModelId",
                table: "Songs",
                column: "ArtistModelId",
                principalTable: "Artists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Artists_ArtistModelId",
                table: "Songs");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Songs_ArtistModelId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "SongLengthInSeconds",
                table: "Songs",
                newName: "Popularity");

            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "Songs",
                newName: "LocalCoverPath");

            migrationBuilder.RenameColumn(
                name: "ArtistModelId",
                table: "Songs",
                newName: "Genre");

            migrationBuilder.AlterColumn<string>(
                name: "PlaylistModelId",
                table: "Songs",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsExplicit",
                table: "Songs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "SpotifyId",
                table: "Playlists",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Playlists",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }
    }
}
