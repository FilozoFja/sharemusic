namespace sharemusic.DTO.PlaylistModel;
public class PlaylistShortModelDTO
{
    public int Id { get; set; }
    public string? SpotifyId { get; set; }
    public required string Name { get; set; }
    public string? CoverImageUrl { get; set; }
}