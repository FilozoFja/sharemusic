namespace sharemusic.Interface
{
    public interface ISpotifyService
    {
        public Task GetPlaylistFromUser(string? accessToken);
    }
}
