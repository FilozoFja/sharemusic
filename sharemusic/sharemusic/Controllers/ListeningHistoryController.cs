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

    }
}
