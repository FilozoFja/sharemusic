using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sharemusic.Interface;
using sharemusic.Models;

namespace sharemusic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListeningHistoryController : ControllerBase
    {
        private readonly IListeningHistoryService _listeningHistoryService;
        public ListeningHistoryController(IListeningHistoryService listeningHistoryService)
        {
            _listeningHistoryService = listeningHistoryService;
        }

        [HttpPost]
        public async Task<ActionResult> AddToHistory(string spotifySongId, int playlistId)
        {
            var listeningHistoryShort = await _listeningHistoryService.AddToHistory(spotifySongId, playlistId);
            return Ok(listeningHistoryShort);
        }
        [HttpGet]
        public IActionResult GetListeningHistory(DateTime start, DateTime end)
        {
            var listeningHistory = _listeningHistoryService.SearchByDate(start, end);
            return Ok(listeningHistory);
        }
        [HttpGet("top-song")]
        public IActionResult GetTopListenedSongs(int top)
        {
            var topListenedSongs = _listeningHistoryService.GetTopListened(top);
            return Ok(topListenedSongs);
        }
        [HttpGet("top-artists")]
        public async Task<IActionResult> GetTopListenedArtists(int top)
        {
            var topArtists = await _listeningHistoryService.GetTopListenedArtists(top);
            return Ok(topArtists);
        }

        [HttpGet("top-genres")]
        public async Task<IActionResult> GetTopListenedGenres(int top)
        {
            var topGenres = await _listeningHistoryService.GetTopListenedGenres(top);
            return Ok(topGenres);
        }
        [HttpGet("history/{take}")]
        public async Task<IActionResult> GetRecentListeningHistory(int take)
        {
            var recentHistory = await _listeningHistoryService.GetRecentListeningHistory(take);
            return Ok(recentHistory);
        }
    }
}
