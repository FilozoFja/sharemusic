namespace sharemusic.Interface;
using sharemusic.Models;
using SpotifyAPI.Web;

public interface ISpotifyService
{
    public Task DownloadPlaylistFromUser();
    public Task DownloadSongsFromUserPlaylist(string playlistId);
    public Task<ArtistModel> GetOrCreateArtistAsync(string artistId, SpotifyClient spotify);
}

