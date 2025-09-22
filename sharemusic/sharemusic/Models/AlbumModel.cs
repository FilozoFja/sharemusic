using System.ComponentModel.DataAnnotations;

namespace sharemusic.Models
{
    public class AlbumModel
    {
        [Required]
        [Key]
        public required string SpotifyId { get; set; }
        public required string Name { get; set; }
        public string? CoverImageUrl { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? ArtistSpotifyId { get; set; }
        public ArtistModel? Artist { get; set; }
        public virtual ICollection<SongModel> Songs { get; set; } = new List<SongModel>();
    }
}
