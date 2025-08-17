namespace sharemusic.Controllers;

using sharemusic.Interface;
using sharemusic.DTOs;
using Microsoft.AspNetCore.Mvc;
using sharemusic.Db;

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
    ///  Gettings artist by id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetArtist([FromRoute] string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("Artist ID is required.");

        var artist = _artistService.GetArtistAsync(id);

        if (artist == null)
            return NotFound(new { Message = "Artist not found." });

        return Ok(artist);
    }
    [HttpGet("spotify/{spotifyId}")]
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
/// Adding a new artist
/// </summary>
/// <param name="artist"></param>
/// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddArtist([FromRoute] ArtistModelDTO artist)
    {
        if (artist == null)
        {
            return BadRequest("Artist data is required.");
        }
        await _artistService.AddArtistAsync(artist);
        return CreatedAtAction(nameof(GetArtist), new { id = artist.Id }, artist);
    }

}