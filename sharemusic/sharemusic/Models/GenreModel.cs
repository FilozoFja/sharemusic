namespace sharemusic.Models
{
    public class GenreModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<SongModel> Songs { get; set; }
    }
}
