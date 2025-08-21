using System.ComponentModel.DataAnnotations;

namespace sharemusic.DTO.ArtistModel;

public class ArtistShortModelDTO
{
    [Key]
    public required string SpotifyId { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
}