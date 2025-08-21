using AutoMapper;
using sharemusic.Db;
using sharemusic.DTO.ListeningHistory;
using sharemusic.Interface;
using sharemusic.Models;

namespace sharemusic.Service
{
    public class ListeningHistoryService : IListeningHistoryService
    {
        private readonly MusicDbContext _musicDbContext;
        private readonly ISongService _songService;
        private readonly IPlaylistService _playlistService;
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;
        public ListeningHistoryService(MusicDbContext musicDbContext, ISongService songService,
                                        IPlaylistService playlistService, IGenreService genreService, IMapper mapper)
        {
            _musicDbContext = musicDbContext;
            _songService = songService;
            _playlistService = playlistService;
            _genreService = genreService;
            _mapper = mapper;
        }

        public async Task<ListeningHistoryModelDTO> AddToHistory(string spotifySongId, int playlistId)
        {
            if (string.IsNullOrEmpty(spotifySongId))
            {
                throw new ArgumentException("Either spotifySongId or spotifyPlaylistId must be provided.");
            }
            var song = await _songService.GetSongBySpotifyIdAsync(spotifySongId);
            var playlist = await _playlistService.GetPlaylistByIdAsync(playlistId);
            var genres = await _genreService.GetGenresBySongIdAsync(spotifySongId);

            if (song == null && playlist == null)
            {
                throw new ArgumentException("Either song or playlist must exist.");
            }
            var history = new Models.ListeningHistoryModel
            {
                Id = Guid.NewGuid().ToString(),
                DateTime = DateTime.UtcNow,
                Song = song,
                Playlist = playlist,
                Genre = genres
            };
            _musicDbContext.ListeningHistory.Add(history);
            await _musicDbContext.SaveChangesAsync();

            return _mapper.Map<ListeningHistoryModelDTO>(history); ;

        }
    }
}
