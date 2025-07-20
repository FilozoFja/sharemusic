namespace sharemusic.Models
{
    public class SongModel
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Artist { get; set; }
        public string? Album { get; set; }
        public string? Genre { get; set; }
        public bool IsDraft { get; set; }


        public string? SpotifyId { get; set; }               
        public int? Popularity { get; set; }               
        public bool IsExplicit { get; set; }              


        public string? LocalSongPath { get; set; }          
        public string? LocalCoverPath { get; set; }         


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        public bool HasLocalFile => !string.IsNullOrEmpty(LocalSongPath) && File.Exists(LocalSongPath);
        public bool HasCover => !string.IsNullOrEmpty(LocalCoverPath) || HasLocalCover;
        public bool HasLocalCover => !string.IsNullOrEmpty(LocalCoverPath) && File.Exists(LocalCoverPath);
        public bool IsComplete => !IsDraft && HasLocalFile;
        public bool IsFromSpotify => !string.IsNullOrEmpty(SpotifyId);


        public string DisplayArtist => Artist ?? "Nieznany artysta";
        public string DisplayAlbum => Album ?? "Nieznany album";
    }
}
