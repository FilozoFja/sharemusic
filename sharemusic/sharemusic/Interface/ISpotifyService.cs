namespace sharemusic.Interface;
using sharemusic.Models;
public interface ISpotifyService
{
    public Task DownloadPlaylistFromUser();
    public Task DownloadSongsFromUserPlaylist(string playlistId);
    public Task<ArtistModel> GetOrCreateArtistAsync(string artistId);
}

