using sharemusic.DTO;
using sharemusic.DTO.PlaylistModel;
using sharemusic.Models;
using SpotifyAPI.Web;

namespace sharemusic.Interface
{
    public interface IPlaylistService
    {
        public Task AddPlaylistAsync(FullPlaylist playlistToAdd);
        public Task<PlaylistModel> AddPlaylistAsync(PlaylistModelDTO playlistToAdd, IFormFile coverImage);
        public Task AddSongToPlaylistAsync(int playlistId, string songId);
        public Task DeleteSongFromPlaylistAsync(int playlistId, int songId);
        public Task<PlaylistModel> GetPlaylistByIdAsync(int id);
        public Task<List<PlaylistShortModelDTO>> GetAllPlaylistsAsync();
        public Task<List<PlaylistShortModelDTO>> GetPlaylistByNameAsync(string name, int? take = null);
        public Task<PlaylistShortModelDTO> GetPlaylistBySpotifyIdAsync(string spotifyId);
        public Task<PlaylistModel> UpdatePlaylistAsync(int id, PlaylistModelDTO updatedPlaylist);
        public Task DeletePlaylistAsync(int id);
        public Task<PlaylistModel> UpdatePlaylistCoverAsync(int id, IFormFile coverImage);
        public Task SetPlaylistFetching(bool isFetched, int playlistId);
    }
}