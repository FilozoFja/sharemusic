using AutoMapper;
using sharemusic.Db;
using sharemusic.DTO.ListeningHistory;
using sharemusic.Interface;
using sharemusic.Models;
using Microsoft.EntityFrameworkCore;
using sharemusic.DTO.ArtistModel;
using sharemusic.DTO.SongModel;
using sharemusic.DTO.GenreModel;

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

            return _mapper.Map<ListeningHistoryModelDTO>(history);
        }

        public async Task<List<ListeningHistoryModelDTO>> SearchByDate(DateTime start, DateTime? end)
        {

            var query = _musicDbContext.ListeningHistory
                .Include(h => h.Song)
                .Include(h => h.Playlist)
                .Include(h => h.Genre)
                .Where(h => h.DateTime >= start);

            if (end.HasValue)
            {
                query = query.Where(h => h.DateTime <= end);
            }

            var results = await query.ToListAsync();
            return _mapper.Map<List<ListeningHistoryModelDTO>>(results);
        }

        public async Task<List<SongShortModelDTO>> GetTopListenedSong(int top)
        {
            var query = await _musicDbContext.ListeningHistory
                .Include(h => h.Song)
                .Where(h => h.Song != null)
                .GroupBy(h => h.Song.SpotifyId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.OrderByDescending(h => h.DateTime).FirstOrDefault())
                .Take(top)
                .ToListAsync();
            var songs = await _musicDbContext.Songs
                .Where(s => query.Select(h => h.Song.SpotifyId).Contains(s.SpotifyId))
                .ToListAsync();

            return _mapper.Map<List<SongShortModelDTO>>(songs);
        }

        public async Task<List<ArtistShortModelDTO>> GetTopListenedArtists(int top)
        {
            var topArtistNames = await _musicDbContext.ListeningHistory
                .Include(h => h.Song)
                .Where(h => h.Song != null && !string.IsNullOrEmpty(h.Song.Artist))
                .GroupBy(h => h.Song.Artist) 
                .OrderByDescending(g => g.Count())
                .Take(top)
                .Select(g => g.Key) 
                .ToListAsync();

            var artists = await _musicDbContext.Artists
                .Where(a => topArtistNames.Contains(a.Name))
                .ToListAsync();

            return _mapper.Map<List<ArtistShortModelDTO>>(artists);
        }

        public async Task<List<GenreShortModelDTO>> GetTopListenedGenres(int top)
        {
            var topGenres = await _musicDbContext.ListeningHistory
                .Include(h => h.Genre)
                .Where(h => h.Genre != null && h.Genre.Any())
                .SelectMany(h => h.Genre)
                .GroupBy(genre => genre.Name)
                .OrderByDescending(g => g.Count())
                .Take(top)
                .Select(g => g.First()) 
                .ToListAsync();

            return _mapper.Map<List<GenreShortModelDTO>>(topGenres);
        }

        public async Task<List<ListeningHistoryModelDTO>> GetRecentListeningHistory(int take)
        {
            var query = await _musicDbContext.ListeningHistory
                .Include(h => h.Song)
                .Include(h => h.Playlist)
                .Include(h => h.Genre)
                .OrderByDescending(h => h.DateTime)
                .Take(take)
                .ToListAsync();

            return _mapper.Map<List<ListeningHistoryModelDTO>>(query);
        }
    }
}