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
/// <summary>
/// Gets a playlist by its ID.
/// </summary>
        [HttpGet("{id}")]
        public async Task<PlaylistModel> GetPlaylistByIdAsync(int id)
        {
            return await _playlistService.GetPlaylistByIdAsync(id);
        }
/// <summary>W
/// Gets a list of playlists by name.
/// </summary>
        [HttpGet("playlist/{name}")]
        public async Task<List<PlaylistShortModelDTO>> GetPlaylistByNameAsync(string name)
        {
            return await _playlistService.GetPlaylistByNameAsync(name);
        }
/// <summary>
/// Gets all playlists short model;
/// </summary>
/// <returns></returns>
        [HttpGet]
        public async Task<List<PlaylistShortModelDTO>> GetAllPlaylistsAsync()
        {
            return await _playlistService.GetAllPlaylistsAsync();
        }
/// <summary>
/// Adds a new playlist.
/// </summary>
        [HttpPost]
        public async Task<IActionResult> AddPlaylistAsync([FromBody] PlaylistModelDTO playlistToAdd)
        {
            if (playlistToAdd == null)
            {
                return BadRequest("Playlist data is null.");
            }
            var playlist = await _playlistService.AddPlaylistAsync(playlistToAdd);
            return Ok(playlist);
        }
/// <summary>
///  Adds a song to a playlist.
/// </summary>
        [HttpPost("{playlistId}/songs/{songId}")]
        public async Task<IActionResult> AddSongToPlaylistAsync(int playlistId,int songId)
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
/// <summary>
///  Deletes a song from a playlist.
/// </summary>
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
/// <summary>
/// Update playlist by ID.
/// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlaylistAsync(int id, [FromBody] PlaylistModelDTO updatedPlaylist)
        {
            if (updatedPlaylist == null)
            {
                return BadRequest("Updated playlist data is null.");
            }
            try
            {
                var playlist = await _playlistService.UpdatePlaylistAsync(id, updatedPlaylist);
                return Ok(playlist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
/// <summary>
/// Deletes a playlist by ID.
/// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylistAsync(int id)
        {
            try
            {
                await _playlistService.DeletePlaylistAsync(id);
                return Ok("Playlist deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}