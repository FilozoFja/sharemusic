using Microsoft.EntityFrameworkCore;
using sharemusic.Db;
using sharemusic.Interface;
using sharemusic.Models;
using sharemusic.DTO;
using AutoMapper;

namespace sharemusic.Service
{
    public class SongService : ISongService
    {
        private readonly MusicDbContext _dbContext;
        private readonly IMapper _mapper;
        public SongService(MusicDbContext dbContext, IMapper mapper) {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddSongAsync(SongModelDTO songDTO)
        {
            await _dbContext.Songs.AddAsync(_mapper.Map<SongModel>(songDTO));
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSongAsync(string id)
        {
            var song = await _dbContext.Songs.FindAsync(id);
            if (song != null)
            {
                _dbContext.Songs.Remove(song);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task EditSongAsync(SongModelDTO songDTO, string id)
        {
            var song = await GetSongById(id);
            var songNew = _mapper.Map<SongModel>(songDTO);
            song.Title = songNew.Title;
            song.Artist = songNew.Artist;
            song.Album = songNew.Album;
            song.Genre = songNew.Genre;
            song.IsDraft = songNew.IsDraft;
            _dbContext.Entry(song).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public async Task EditSongURLAsync(string id, string url)
        {
            var song = await GetSongById(id);
            song.LocalSongPath = url;
            _dbContext.SaveChanges();
        }

        public async Task<SongModel?> GetSongById(string id)
        {
            return await _dbContext.Songs.FindAsync(id);
        }
    }
}
