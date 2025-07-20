using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sharemusic.DTO;
using sharemusic.Interface;
using sharemusic.Models;

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

        [HttpPost]
        public IActionResult AddSongDraft(SongModelDTO songModelDTO)
        {
            _songService.AddSongDraft(
                songModelDTO.Title,
                songModelDTO.Artist,
                songModelDTO.Album,
                songModelDTO.Genre,
                songModelDTO.CoverImageUrl,
                songModelDTO.SongUrl,
                songModelDTO.IsDraft
            );

            return Ok(new { message = "Song draft added successfully." });
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSongDraft(int id)
        {
            _songService.DeleteSongDraft(id);
            return Ok(new { message = "Song draft deleted successfully." });
        }
        [HttpPut]
        public IActionResult EditSong(SongModel songModelNew)
        {
            _songService.EditSong(songModelNew);
            return Ok(new { message = "Song edited successfully." });
        }
        [HttpPut("{id}/{songUrl}")]
        public IActionResult AddUrlToSong(int id, string songUrl)
        {
            var song = _songService.GetSongById(id);
            if (song == null)
            {
                return NotFound(new { message = "Song not found." });
            }
            _songService.EditSong(song);
            return Ok(new { message = "Song URL added successfully." });
        }
        [HttpGet("{id}")]
        public IActionResult GetSongById(int id)
        {
            var song = _songService.GetSongById(id);
            if (song == null)
            {
                return NotFound(new { message = "Song not found." });
            }
            return Ok(song);
        }
    }
}
