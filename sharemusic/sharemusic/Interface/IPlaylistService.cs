using sharemusic.DTO;
using sharemusic.DTO.PlaylistModel;
using sharemusic.Models;
using SpotifyAPI.Web;

namespace sharemusic.Interface
{
    public interface IPlaylistService
    {
        public Task AddPlaylist(FullPlaylist playlistToAdd);
        public Task AddPlaylist(PlaylistModelDTO playlistToAdd);
        public Task AddSongToPlaylistAsync(int playlistId, int songId);
        public Task DeleteSongFromPlaylistAsync(int playlistId, int songId);
        public Task<PlaylistModel> GetPlaylistByIdAsync(int id);
        public Task<List<PlaylistShortModelDTO>> GetAllPlaylistsAsync();
        public Task<List<PlaylistShortModelDTO?>> GetPlaylistByNameAsync(string name);
        public Task UpdatePlaylistAsync(int id, PlaylistModelDTO updatedPlaylist);
    }
}