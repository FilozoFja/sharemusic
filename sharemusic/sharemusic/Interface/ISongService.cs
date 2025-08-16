using sharemusic.DTO;
using sharemusic.Models;
using sharemusic.DTO.SongModel;

namespace sharemusic.Interface
{
    public interface ISongService
    {
        public Task DeleteSongAsync(int id);
        public Task EditSongAsync(SongModelDTO songDTO, int id);
        public Task<SongModel?> GetSongByIdAsync(int id);

        public Task<List<SongShortModelDTO>> GetSongByNameAsync(string name, int? take = null);
        public Task<List<SongShortModelDTO>> GetAllSongsAsync();

        public Task AddSongLengthAndURLAsync(int id, int songLengthInSeconds, string url);
    }
}
