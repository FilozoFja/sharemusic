using sharemusic.DTO;
using sharemusic.Models;
using sharemusic.DTO.SongModel;

namespace sharemusic.Interface
{
    public interface ISongService
    {
        public Task<SongModel> GetSongBySpotifyIdAsync(string spotifyId);
        public Task<List<SongShortModelDTO>> GetSongsByNameAsync(string name, int? take = null);
        public Task<List<SongShortModelDTO>> GetAllSongsAsync();
        public Task AddSongLengthAndURLAsync(string spotifyId, int songLengthInSeconds, string url);
        public Task DeleteSongAsync(string ispotifyIdd);
        public Task EditSongAsync(SongModelDTO songDTO, string spotifyId);
    }
}
