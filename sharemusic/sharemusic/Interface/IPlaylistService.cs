using sharemusic.DTO;
using sharemusic.DTO.PlaylistModel;
using sharemusic.Models;
using SpotifyAPI.Web;

namespace sharemusic.Interface
{
    public interface IPlaylistService
    {
        public Task AddPlaylistAsync(FullPlaylist playlistToAdd);
        public Task<PlaylistModel> AddPlaylistAsync(PlaylistModelDTO playlistToAdd);
        public Task AddSongToPlaylistAsync(int playlistId, int songId);
        public Task DeleteSongFromPlaylistAsync(int playlistId, int songId);
        public Task<PlaylistModel> GetPlaylistByIdAsync(int id);
        public Task<List<PlaylistShortModelDTO>> GetAllPlaylistsAsync();
        public Task<List<PlaylistShortModelDTO>> GetPlaylistByNameAsync(string name);
        public Task<PlaylistModel> UpdatePlaylistAsync(int id, PlaylistModelDTO updatedPlaylist);
        public Task DeletePlaylistAsync(int id);
    }
}