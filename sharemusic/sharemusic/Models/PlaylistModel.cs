namespace sharemusic.Models
{
    public class PlaylistModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int SongId { get; set; }
        public string? Description { get; set; }
        public string? CoverUrl { get; set; }

        public List<SongModel> Songs { get; set; }
    }
}
