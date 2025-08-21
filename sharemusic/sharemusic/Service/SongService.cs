using Microsoft.EntityFrameworkCore;
using sharemusic.Db;
using sharemusic.Interface;
using sharemusic.Models;
using sharemusic.DTO;
using AutoMapper;
using sharemusic.DTO.SongModel;

namespace sharemusic.Service
{
    public class SongService : ISongService
    {
        private readonly MusicDbContext _dbContext;
        private readonly IMapper _mapper;
        public SongService(MusicDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        /// <summary>
        /// Set location and song lenght manually.
        /// </summary>
        public async Task AddSongLengthAndURLAsync(string spotifyId, int songLengthInSeconds, string url)
        {
            var song = await GetSongBySpotifyIdAsync(spotifyId);
            if (song != null)
            {
                song.SongLengthInSeconds = songLengthInSeconds;
                song.LocalSongPath = url;
                song.IsDraft = false;
                _dbContext.Entry(song).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Delete song by spotify ID.
        /// </summary>
        public async Task DeleteSongAsync(string spotifyId)
        {
            var song = await _dbContext.Songs.FindAsync(spotifyId);
            if (song != null)
            {
                _dbContext.Songs.Remove(song);
                await _dbContext.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Edit song details by spotify ID.
        /// </summary>
        public async Task EditSongAsync(SongModelDTO songDTO, string spotifyId)
        {
            var song = await GetSongBySpotifyIdAsync(spotifyId);
            if (song == null)
            {
                throw new Exception("Song not found.");
            }

            song.Title = songDTO.Title != null ? songDTO.Title : throw new Exception("Title cannot be null");
            song.Artist = songDTO.Artist;
            song.Album = songDTO.Album;
            song.IsDraft = songDTO.IsDraft != null ? songDTO.IsDraft.Value : true;
            song.SongLengthInSeconds = songDTO.SongLengthInSeconds;
            song.LocalSongPath = songDTO.LocalSongPath;
            song.ArtistSpotifyId = songDTO.ArtistSpotifyId;
            song.CoverImageUrl = songDTO.CoverImageUrl;
            song.ReleaseDate = songDTO.ReleaseDate;
            _dbContext.Entry(song).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Getting all songs.
        /// </summary>
        public async Task<List<SongShortModelDTO>> GetAllSongsAsync()
        {
            var songs = await _dbContext.Songs.ToListAsync();
            if (songs == null || !songs.Any())
            {
                throw new Exception("No songs found.");
            }
            return _mapper.Map<List<SongShortModelDTO>>(songs);
        }
        /// <summary>
        /// Get song by spotify ID.
        /// </summary>
        public async Task<SongModel> GetSongBySpotifyIdAsync(string spotifyId)
        {
            var song = await _dbContext.Songs.FindAsync(spotifyId);
            if (song == null)
            {
                throw new Exception("Song not found.");
            }
            return song;
        }
        /// <summary>
        /// Getting songs by name. 
        /// </summary>
        public async Task<List<SongShortModelDTO>> GetSongsByNameAsync(string name, int? take = null)
        {
            var query = _dbContext.Songs
                .Where(s => EF.Functions.Like(s.Title, $"%{name}%"));

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            var songs = await query.ToListAsync();

            return _mapper.Map<List<SongShortModelDTO>>(songs);
        }
    }
}
