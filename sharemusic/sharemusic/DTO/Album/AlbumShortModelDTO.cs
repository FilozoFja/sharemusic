namespace sharemusic.DTO.Album
{
    public class AlbumShortModelDTO
    {
        public required string SpotifyId { get; set; }
        public required string Name { get; set; }
        public string? CoverImageUrl { get; set; }
    }
}
