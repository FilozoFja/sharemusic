using System.ComponentModel.DataAnnotations;

namespace sharemusic.Models
{
    public class PlaylistModel
    {
        [Key] 
        public int Id { get; set; }
        public string? SpotifyId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? CoverUrl { get; set; }
        public List<SongModel> Songs { get; set; } = new List<SongModel>();
    }
}