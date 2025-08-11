namespace sharemusic.Controllers;

using sharemusic.Interface;
using sharemusic.DTOs;
using Microsoft.AspNetCore.Mvc;

public class ArtistController : ControllerBase
{
    private readonly IArtistService _artistService;

    public ArtistController(IArtistService artistService)
    {
        _artistService = artistService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetArtist(string id)
    {
        var artist = await _artistService.GetArtistAsync(id);
        if (artist == null)
        {
            return NotFound();
        }
        return Ok(artist);
    }

    [HttpGet("{id}/songs")]
    public async Task<IActionResult> GetAllSongsFromArtist(string id)
    {
        var songs = await _artistService.GetAllSongsFromArtistAsync(id);
        return Ok(songs);
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetArtistByName(string name)
    {
        var artists = await _artistService.GetArtistByNameAsync(name);
        return Ok(artists);
    }

    [HttpPost]
    public async Task<IActionResult> AddArtist([FromBody] ArtistModelDTO artist)
    {
        if (artist == null)
        {
            return BadRequest("Artist data is required.");
        }
        await _artistService.AddArtistAsync(artist);
        return CreatedAtAction(nameof(GetArtist), new { id = artist.Id }, artist);
    }
}