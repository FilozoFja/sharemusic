using sharemusic.Models;

namespace sharemusic.Interface
{
    public interface ISongService
    {
        public void AddSongDraft(string title, string? artist, string? album, string? genre, string? coverImageUrl, string? songUrl, bool isDraft);
        public void DeleteSongDraft(int id);
        public void EditSong(SongModel songModelNew);
        public SongModel? GetSongById(int id); 
    }
}
