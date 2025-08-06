using Microsoft.AspNetCore.Mvc;
using sharemusic.DTO;
using sharemusic.Interface;

namespace sharemusic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;
        private readonly ISpotifyService _spotifyService;
        public SongController(ISongService songService, ISpotifyService spotifyService)
        {
            _songService = songService;
            _spotifyService = spotifyService;
        }

        [HttpPost]
        public async Task<IActionResult> AddSongDraft(SongModelDTO songModelDTO)
        {
            await _songService.AddSongAsync(songModelDTO);

            return Ok(new { message = "Song draft added successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSongDraft(string id)
        {
            await _songService.DeleteSongAsync(id);
            return Ok(new { message = "Song draft deleted successfully." });
        }

        [HttpPut]
        public async Task<IActionResult> EditSong(SongModelDTO songModelDTO, string id)
        {
            await _songService.EditSongAsync(songModelDTO, id);
            return Ok(new { message = "Song edited successfully." });
        }

        [HttpPut("{id}/{songUrl}")]
        public async Task<IActionResult> AddUrlToSong(string id, string songUrl)
        {
            var song = _songService.GetSongById(id);
            if (song == null){return NotFound(new { message = "Song not found." });}
            await _songService.EditSongURLAsync(id,songUrl);

            return Ok(new { message = "Song URL added successfully." });
        }
        [HttpGet("{id}")]
        public IActionResult GetSongById(string id)
        {
            var song = _songService.GetSongById(id);
            if (song == null)
            {
                return NotFound(new { message = "Song not found." });
            }
            return Ok(song);
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
