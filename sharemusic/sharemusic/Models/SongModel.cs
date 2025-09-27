using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace sharemusic.Models
{
    public class SongModel
    {
        [Key]
        public required string SpotifyId { get; set; }
        public required string Title { get; set; }
        public string? Artist { get; set; }
        public string? ArtistSpotifyId { get; set; }
        public string? AlbumName { get; set; }
        public string? AlbumId { get; set; }
        [JsonIgnore]
        public AlbumModel Album { get; set; }

        public bool IsDraft { get; set; }
        public string? CoverImageUrl { get; set; }          
        public int? SongLengthInSeconds { get; set; }

        public string? LocalSongPath { get; set; }               
        public DateTime? ReleaseDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        public bool HasLocalFile => !string.IsNullOrEmpty(LocalSongPath) && File.Exists(LocalSongPath);
        public bool IsComplete => !IsDraft && HasLocalFile;


        public string DisplayArtist => Artist ?? "Nieznany artysta";
        public string DisplayAlbum => AlbumName ?? "Nieznany album";

    }
}
