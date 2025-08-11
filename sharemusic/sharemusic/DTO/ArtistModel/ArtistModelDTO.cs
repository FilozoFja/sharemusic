namespace sharemusic.DTOs
{
    public class ArtistModelDTO
    {
        public string Id { get; set; } = string.Empty;
        public string SpotifyId { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public List<string?> Genres { get; set; } = new();
    }
}
