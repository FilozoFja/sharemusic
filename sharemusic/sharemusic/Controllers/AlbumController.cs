using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sharemusic.Interface;

namespace sharemusic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAlbums()
        {
            var albums = await _albumService.GetAllAlbums();
            return Ok(albums);
        }

        [HttpGet("{spotifyId}")]
        public async Task<IActionResult> GetAlbumBySpotifyId(string spotifyId)
        {
            var album = await _albumService.GetAlbumBySpotifyId(spotifyId);
            return Ok(album);
        }
    }
}
