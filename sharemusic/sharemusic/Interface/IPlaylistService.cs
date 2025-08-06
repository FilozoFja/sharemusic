using SpotifyAPI.Web;

namespace sharemusic.Interface
{
    public interface IPlaylistService
    {
        public Task AddPlaylist(FullPlaylist playlistToAdd);
    }
}