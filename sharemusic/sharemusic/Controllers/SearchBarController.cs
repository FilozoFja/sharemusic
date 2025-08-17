using Microsoft.AspNetCore.Mvc;
using sharemusic.Interface;

namespace sharemusic.Controller;
[Route("api/[controller]")]
[ApiController]
public class SearchBarController : ControllerBase
{
    private readonly IArtistService _artistService;
    private readonly ISongService _songService;
    private readonly IPlaylistService _playlistService;
    public SearchBarController(IArtistService artistService, ISongService songService, IPlaylistService playlistService)
    {
        _artistService = artistService;
        _songService = songService;
        _playlistService = playlistService;
    }

    [HttpGet]
    public async Task<ActionResult<SearchBarResponseDTO>> Search(string name, int take)
    {
        var artists = await _artistService.GetArtistsByNameAsync(name, take);
        var song = await _songService.GetSongByNameAsync(name, take);
        var playlist = await _playlistService.GetPlaylistByNameAsync(name, take);

        SearchBarResponseDTO response = new SearchBarResponseDTO
        {
            Artist = artists,
            Songs = song,
            Playlists = playlist
        };
        return Ok(response);
    }
}