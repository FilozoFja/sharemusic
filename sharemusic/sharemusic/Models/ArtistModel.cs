using System.ComponentModel.DataAnnotations;

namespace sharemusic.Models;

public class ArtistModel
{
    [Key]
    [Required]
    public string Id { get; set; }
    [Required]
    public required string SpotifyId { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public List<string?> Genres { get; set; } = [];

    public List<SongModel> Songs { get; set; } = new();
}