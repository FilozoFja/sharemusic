using Microsoft.EntityFrameworkCore;
using sharemusic.Models;
namespace sharemusic.Db
{
    public class MusicDbContext : DbContext
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options)
        {
        }

        public DbSet<Models.SongModel> Songs { get; set; }
        public DbSet<Models.PlaylistModel> Playlists { get; set; }
        public DbSet<Models.SpotifyTokenRequestModel> SpotifyTokens { get; set; }
        public DbSet<Models.ArtistModel> Artists { get; set; }
        public DbSet<Models.GenreModel> Genres { get; set; }
        public DbSet<Models.ListeningHistoryModel> ListeningHistory { get; set; }
        public DbSet<Models.UserModel> Users { get; set; }
        public DbSet<Models.AlbumModel> Albums { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AlbumModel>()
               .HasOne(a => a.Artist)
               .WithMany(ar => ar.Albums)
               .HasForeignKey(a => a.ArtistSpotifyId)
               .HasPrincipalKey(ar => ar.SpotifyId);

            modelBuilder.Entity<SongModel>()
                .HasOne(s => s.Album)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.AlbumId);
        }
    }
}