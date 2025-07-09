namespace sharemusic.DTO
{
    public class SongModelDTO
    {
        public required string Title { get; set; }
        public string? Artist { get; set; }
        public string? Album { get; set; }
        public string? Genre { get; set; }
        public string? CoverImageUrl { get; set; }
        public string? SongUrl { get; set; }
        public bool IsDraft { get; set; }
    }
}
