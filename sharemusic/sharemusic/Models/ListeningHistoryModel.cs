using sharemusic.DTO.PlaylistModel;
using sharemusic.DTO.SongModel;

namespace sharemusic.Models
{
    public class ListeningHistoryModel
    {
        public string Id { get; set; }
        public DateTime DateTime { get; set; }
        public SongModel Song { get; set; }
        public PlaylistModel Playlist { get; set; }
        public List<GenreModel>? Genre { get; set; }
    }
}
