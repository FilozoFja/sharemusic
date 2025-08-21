using sharemusic.DTO.GenreModel;
using sharemusic.DTO.PlaylistModel;
using sharemusic.DTO.SongModel;

namespace sharemusic.DTO.ListeningHistory
{
    public class ListeningHistoryModelDTO
    {
        public string Id { get; set; }
        public DateTime DateTime { get; set; }
        public SongShortModelDTO SongShort { get; set; }
        public PlaylistShortModelDTO PlaylistShort { get; set; }
        public List<GenreShortModelDTO>? GenreShortModelDTO { get; set; }
    }
}
