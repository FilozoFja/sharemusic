namespace sharemusic.DTO.SongModel;

public class SongShortModelDTO
{
    public required int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; } = string.Empty;
    public int? SongLengthInSeconds { get; set; }
    public DateTime? ReleaseDate { get; set; }
}