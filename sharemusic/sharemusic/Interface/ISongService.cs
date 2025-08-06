using sharemusic.Models;
using sharemusic.DTO;

namespace sharemusic.Interface
{
    public interface ISongService
    {
        public Task AddSongAsync(SongModelDTO songDTO);
        public Task DeleteSongAsync(string id);
        public Task EditSongAsync(SongModelDTO songDTO, string id);
        public Task<SongModel?> GetSongById(string id);
        public Task EditSongURLAsync(string id, string url);
    }
}
