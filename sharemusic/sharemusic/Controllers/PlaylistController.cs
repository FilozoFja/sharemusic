using Microsoft.AspNetCore.Mvc;
using sharemusic.DTO;
using sharemusic.DTO.PlaylistModel;
using sharemusic.Interface;
using sharemusic.Models;

namespace sharemusic.Controller {
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;
        public PlaylistController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }
        [HttpGet("{id}")]
        public async Task<PlaylistModel> GetPlaylistByIdAsync([FromQuery] int id)
        {
            return await _playlistService.GetPlaylistByIdAsync(id);
        }
        [HttpGet("playlist/{name}")]
        public async Task<List<PlaylistShortModelDTO?>> GetPlaylistByNameAsync([FromQuery] string name)
        {
            return await _playlistService.GetPlaylistByNameAsync(name);
        }
        [HttpGet]
        public async Task<List<PlaylistShortModelDTO>> GetAllPlaylistsAsync()
        {
            return await _playlistService.GetAllPlaylistsAsync();
        }
        [HttpPost]
        public async Task<IActionResult> AddPlaylist([FromBody] PlaylistModelDTO playlistToAdd)
        {
            if (playlistToAdd == null)
            {
                return BadRequest("Playlist data is null.");
            }
            await _playlistService.AddPlaylist(playlistToAdd);
            return Ok("Playlist added successfully.");
        }
        [HttpPost("{playlistId}/songs/{songId}")]
        public async Task<IActionResult> AddSongToPlaylistAsync([FromQuery] int playlistId, [FromQuery] int songId)
        {
            try
            {
                await _playlistService.AddSongToPlaylistAsync(playlistId, songId);
                return Ok("Song added to playlist successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("{playlistId}/songs/{songId}")]
        public async Task<IActionResult> DeleteSongFromPlaylistAsync(int playlistId, int songId)
        {
            try
            {
                await _playlistService.DeleteSongFromPlaylistAsync(playlistId, songId);
                return Ok("Song deleted from playlist successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlaylistAsync(int id, [FromBody] PlaylistModelDTO updatedPlaylist)
        {
            if (updatedPlaylist == null)
            {
                return BadRequest("Updated playlist data is null.");
            }
            try
            {
                await _playlistService.UpdatePlaylistAsync(id, updatedPlaylist);
                return Ok("Playlist updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}