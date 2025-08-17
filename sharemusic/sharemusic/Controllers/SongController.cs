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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSongAsync(int id)
        {
            await _songService.DeleteSongAsync(id);
            return Ok(new { message = "Song draft deleted successfully." });
        }

        [HttpPut]
        public async Task<IActionResult> EditSongAsync(SongModelDTO songModelDTO, int id)
        {
            await _songService.EditSongAsync(songModelDTO, id);
            return Ok(new { message = "Song edited successfully." });
        }

        [HttpGet("{id}")]
        public IActionResult GetSongById(int id)
        {
            var song = _songService.GetSongByIdAsync(id);
            if (song == null)
            {
                return NotFound(new { message = "Song not found." });
            }
            return Ok(song);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSongsAsync()
        {
            return Ok(await _songService.GetAllSongsAsync());
        }
        [HttpGet("search/song/{name}")]
        public async Task<IActionResult> SearchSongsAsync(string name)
        {
            return Ok(await _songService.GetSongByNameAsync(name));
        }

        [HttpGet("{id}/{url}/{songLengthInSeconds}")]
        public async Task<IActionResult> AddSongLengthAndURLAsync(int id, string url, int songLengthInSeconds)
        {
            await _songService.AddSongLengthAndURLAsync(id, songLengthInSeconds, url);
            return Ok(new { message = "Song length and URL added successfully." });
        }

    }
}
