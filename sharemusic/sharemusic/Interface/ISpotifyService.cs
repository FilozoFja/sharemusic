namespace sharemusic.Interface
{
    public interface ISpotifyService
    {
        public Task DownloadPlaylistFromUser();
        public Task DownloadSongsFromUserPlaylist(string playlistId);
    }
}
