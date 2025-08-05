using System.ComponentModel.DataAnnotations;

namespace sharemusic.Models
{
    public class PlaylistModel
    {
        [Key] 
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string SpotifyId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? CoverUrl { get; set; }
        public List<SongModel> Songs { get; set; } = new List<SongModel>();
    }
}