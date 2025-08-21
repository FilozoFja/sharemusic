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
        public SongController(ISongService songService)
        {
            _songService = songService;
        }
        /// <summary>
        /// Deleting song
        /// </summary>
        [HttpDelete("{spotifyId}")]
        public async Task<IActionResult> DeleteSongAsync(string spotifyId)
        {
            await _songService.DeleteSongAsync(spotifyId);
            return Ok(new { message = "Song draft deleted successfully." });
        }
        /// <summary>
        /// Editing existing song 
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> EditSongAsync(SongModelDTO songModelDTO, string spotifyId)
        {
            await _songService.EditSongAsync(songModelDTO, spotifyId);
            return Ok(new { message = "Song edited successfully." });
        }
        /// <summary>
        /// Get song by spotify id
        /// </summary>
        [HttpGet("{spotifyId}")]
        public IActionResult GetSongById(string spotifyId)
        {
            var song = _songService.GetSongBySpotifyIdAsync(spotifyId);
            if (song == null)
            {
                return NotFound(new { message = "Song not found." });
            }
            return Ok(song);
        }
        /// <summary>
        /// Getting short list of all songs
        /// </summary>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllSongsAsync()
        {
            return Ok(await _songService.GetAllSongsAsync());
        }
        /// <summary>
        /// Searching for song by name
        /// </summary>
        [HttpGet("Search/{name}")]
        public async Task<IActionResult> SearchSongsAsync(string name)
        {
            return Ok(await _songService.GetSongsByNameAsync(name));
        }
        /// <summary>
        /// Setting song length and URL 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/{url}/{songLengthInSeconds}")]
        public async Task<IActionResult> AddSongLengthAndURLAsync(string spotifyId, string url, int songLengthInSeconds)
        {
            await _songService.AddSongLengthAndURLAsync(spotifyId, songLengthInSeconds, url);
            return Ok(new { message = "Song length and URL added successfully." });
        }

    }
}
