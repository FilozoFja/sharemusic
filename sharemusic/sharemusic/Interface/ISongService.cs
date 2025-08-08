using sharemusic.DTO;
using sharemusic.Models;
using sharemusic.DTO.SongModel;

namespace sharemusic.Interface
{
    public interface ISongService
    {
        public Task AddSongAsync(SongModelDTO songDTO);
        public Task DeleteSongAsync(string id);
        public Task EditSongAsync(SongModelDTO songDTO, string id);
        public Task<SongModel?> GetSongByIdAsync(string id);

        public Task<List<SongShortModelDTO>> GetSongByNameAsync(string name);
        public Task<List<SongShortModelDTO>> GetAllSongsAsync();

        public Task AddSongLengthAndURLAsync(string id, int songLengthInSeconds, string url);
    }
}
