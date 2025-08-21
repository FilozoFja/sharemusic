namespace sharemusic.Controllers;

using sharemusic.Interface;
using sharemusic.DTOs;
using Microsoft.AspNetCore.Mvc;

[Route("api/artists")]
[ApiController]
public class ArtistController : ControllerBase
{
    private readonly IArtistService _artistService;

    public ArtistController(IArtistService artistService)
    {
        _artistService = artistService;
    }

    /// <summary>
    ///  Get artist by spotify id
    /// </summary>
    [HttpGet("{spotifyId}")]
    public async Task<IActionResult> GetArtistBySpotifyId(string spotifyId)
    {
        if (string.IsNullOrWhiteSpace(spotifyId))
        {
            return BadRequest("Spotify ID is required.");
        }

        var artist = await _artistService.GetArtistBySpotifyIdAsync(spotifyId);
        if (artist == null)
        {
            return NotFound(new { Message = "Artist not found." });
        }
        return Ok(artist);
    }
    /// <summary>
    /// Getting artists by name
    /// </summary>
    [HttpGet("nameToSearch/{name}")]
    public async Task<IActionResult> GetArtistsByNameAsync([FromRoute]string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest("Artist name is required.");
        }
        var artists = await _artistService.GetArtistsByNameAsync(name);
        if (artists == null || artists.Count == 0)
        {
            return NotFound(new { Message = "No artists found with the given name." });
        }
        return Ok(artists);
    }
    /// <summary>
    /// Update artist information by spotify id
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> UpdateArtistAsync([FromBody] ArtistModelDTO artistDto)
    {
        if (artistDto == null || string.IsNullOrWhiteSpace(artistDto.SpotifyId))
        {
            return BadRequest("Artist data is required.");
        }
        var updatedArtist = await _artistService.UpdateArtistAsync(artistDto.SpotifyId, artistDto);
        return Ok(updatedArtist);
    }

}