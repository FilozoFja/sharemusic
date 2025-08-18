using sharemusic.DTO;
using sharemusic.Models;

namespace sharemusic.DTOs
{
    public class ArtistModelDTO
    {
        public required string SpotifyId { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public List<string?> Genres { get; set; } = [];
        public List<SongModel> Songs { get; set; } = [];
    }
}
