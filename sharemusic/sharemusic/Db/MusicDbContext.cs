using Microsoft.EntityFrameworkCore;

namespace sharemusic.Db
{
    public class MusicDbContext:DbContext
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options)
        {
        }
        public DbSet<Models.SongModel> Songs { get; set; }
        public DbSet<Models.PlaylistModel> Playlists { get; set; }
        public DbSet<Models.SpotifyTokenRequestModel> SpotifyTokens { get; set; }
        public DbSet<Models.ArtistModel> Artists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add any custom model configurations here
        }
    }
}
