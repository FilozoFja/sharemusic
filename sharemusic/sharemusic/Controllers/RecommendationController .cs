using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sharemusic.DTO.Album;
using sharemusic.DTO.SongModel;
using sharemusic.DTO.ArtistModel;
using sharemusic.DTO.PlaylistModel;
using sharemusic.Interface;
using sharemusic.Models;

namespace sharemusic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly IListeningHistoryService _listeningHistoryService;

        public RecommendationController(IListeningHistoryService listeningHistoryService)
        {
            _listeningHistoryService = listeningHistoryService;
        }

        [HttpGet("songs")]
        public async Task<ActionResult<List<SongShortModelDTO>>> RecommendationSelectedSongs()
        {
            var recommendedGenres = await _listeningHistoryService.GetTopListenedGenres(5);
            if (recommendedGenres == null || recommendedGenres.Count == 0)
                return NotFound("No genres found in listening history.");

            var recommendedSongs = new List<SongShortModelDTO>();
            foreach (var genre in recommendedGenres)
            {
                recommendedSongs.AddRange(await _listeningHistoryService.GetLeastPopularSongsByGenre(genre.Name, 1));
            }

            return Ok(recommendedSongs);
        }

        [HttpGet("artists")]
        public async Task<ActionResult<List<ArtistShortModelDTO>>> RecommendationSelectedArtists()
        {
            var recommendedGenres = await _listeningHistoryService.GetTopListenedGenres(5);
            if (recommendedGenres == null || recommendedGenres.Count == 0)
                return NotFound("No genres found in listening history.");

            var recommendedArtists = new List<ArtistShortModelDTO>();
            foreach (var genre in recommendedGenres)
            {
                recommendedArtists.AddRange(await _listeningHistoryService.GetLeastPopularArtistsByGenre(genre.Name, 1));
            }

            return Ok(recommendedArtists);
        }

        [HttpGet("playlists")]
        public async Task<ActionResult<List<PlaylistShortModelDTO>>> RecommendationSelectedPlaylists()
        {
            var recommendedGenres = await _listeningHistoryService.GetTopListenedGenres(5);
            if (recommendedGenres == null || recommendedGenres.Count == 0)
                return NotFound("No genres found in listening history.");

            var recommendedPlaylists = new List<PlaylistShortModelDTO>();
            foreach (var genre in recommendedGenres)
            {
                recommendedPlaylists.AddRange(await _listeningHistoryService.GetLeastPopularPlaylistsByGenre(genre.Name, 1));
            }

            return Ok(recommendedPlaylists);
        }

        [HttpGet("recomendedSongBigBanner")]
        public async Task<ActionResult<SongModel>> RecommendationSelectedASong()
        {
            var song = await _listeningHistoryService.GetRandomSong();
            return Ok(song);
        }
        [HttpGet("getRandomAlbums")]
        public async Task<ActionResult<List<AlbumShortModelDTO>>> RecommendationSelectedAlbums(int top)
        {
            var albums = await _listeningHistoryService.GetRandomAlbum(top);
            return Ok(albums);
        }
    }
}
