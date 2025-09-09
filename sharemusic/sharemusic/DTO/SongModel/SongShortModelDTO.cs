namespace sharemusic.DTO.SongModel;

public class SongShortModelDTO
{
    public required string SpotifyId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; } = string.Empty;
    public int? SongLengthInSeconds { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? Artist { get; set; } = string.Empty;
    public string? Album { get; set; } = string.Empty;
}