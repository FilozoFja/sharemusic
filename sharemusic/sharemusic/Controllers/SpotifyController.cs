using Microsoft.AspNetCore.Mvc;
using sharemusic.Interface;

namespace sharemusic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyController : ControllerBase
    {
        private readonly ISpotifyService _spotifyService;
        public SpotifyController(ISpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        [HttpPost("downloadFromSpotify/")]
        public async Task<IActionResult> GetWholePlaylistFromSpotify()
        {
            await _spotifyService.DownloadPlaylistFromUser();
            return Ok(new { message = "Playlists fetched successfully." });
        }

        [HttpPost("downloadFromSpotify/{playlistId}")]
        public async Task<IActionResult> DownloadDraftSongFromSpotifyPlaylisy(string playlistId)
        {
            await _spotifyService.DownloadSongsFromUserPlaylist(playlistId);
            return Ok(new { message = "Songs downloaded successfully." });
        }
    }
}