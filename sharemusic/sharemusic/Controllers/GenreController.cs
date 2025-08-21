using Microsoft.AspNetCore.Mvc;
using sharemusic.DTO.GenreModel;
using sharemusic.Interface;
using sharemusic.Models;

namespace sharemusic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        /// <summary>
        /// Retrieves a list of all available genres in short format.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<GenreShortModelDTO>>> GetGenresAsync()
        {
            try
            {
                var genres = await _genreService.GetGenreShortAsync();
                return Ok(genres);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Retrieves detailed information about a specific genre by its ID, including associated songs.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreModel>> GetGenreByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Genre ID cannot be null or empty.");
            }

            try
            {
                var genre = await _genreService.GetGenreById(id);
                return Ok(genre);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Genre with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}