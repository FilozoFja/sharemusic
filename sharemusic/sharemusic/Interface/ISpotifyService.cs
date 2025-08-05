namespace sharemusic.Interface
{
    public interface ISpotifyService
    {
        public Task DownloadPlaylistFromUser(string? accessToken);
        public Task DownloadSongsFromUserPlaylist(string playlistId, string? accessToken);
    }
}
