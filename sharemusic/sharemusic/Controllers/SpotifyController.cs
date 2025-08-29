using Microsoft.AspNetCore.Mvc;
using sharemusic.Interface;

namespace sharemusic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyController : ControllerBase
    {
        private readonly ISpotifyService _spotifyService;
        private readonly IPlaylistService _playlistService;
        public SpotifyController(ISpotifyService spotifyService, IPlaylistService playlistService)
        {
            _spotifyService = spotifyService;
            _playlistService = playlistService;
        }

        [HttpPost("playlist/getAll")]
        public async Task<IActionResult> GetWholePlaylistFromSpotify()
        {
            await _spotifyService.DownloadPlaylistFromUser();
            var playlists = await _playlistService.GetAllPlaylistsAsync();
            return Ok(playlists);
        }

        [HttpPost("playlist/{playlistId}/songs/getAll")]
        public async Task<IActionResult> DownloadDraftSongFromSpotifyPlaylisy(string playlistId)
        {
            await _spotifyService.DownloadSongsFromUserPlaylist(playlistId);
            var playlist = await _playlistService.GetPlaylistBySpotifyIdAsync(playlistId);
            return Ok(playlist);
        }

        [HttpGet("userData")]
        public async Task<IActionResult> GetUserData()
        {
            var user = await _spotifyService.DownloadInfoAboutUser();
            return Ok(user);
        }
    }
}