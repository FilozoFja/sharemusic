namespace sharemusic.DTO.SongModel;

public class SongShortModelDTO
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; } = string.Empty;
    public int? SongLengthInSeconds { get; set; }
}