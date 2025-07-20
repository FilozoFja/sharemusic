using Microsoft.EntityFrameworkCore;
using sharemusic.Db;
using sharemusic.Interface;
using sharemusic.Models;

namespace sharemusic.Service
{
    public class SongService : ISongService
    {
        private readonly MusicDbContext _dbContext;
        public SongService(MusicDbContext dbContext) {
            _dbContext = dbContext;
        }

        public void AddSongDraft(string title, string? artist, string? album, string? genre, string? coverImageUrl, string? songUrl, bool isDraft)
        {
            var song = new Models.SongModel
            {
                Title = title,
                Artist = artist,
                Album = album,
                Genre = genre,
                IsDraft = isDraft
            };

            _dbContext.Songs.Add(song);
            _dbContext.SaveChanges();
        }

        public void DeleteSongDraft(int id)
        {
            var song = _dbContext.Songs.Find(id);
            if (song != null)
            {
                _dbContext.Songs.Remove(song);
                _dbContext.SaveChanges();
            }
        }

        public void EditSong(SongModel songModelNew)
        {
            int id = songModelNew.Id;
            var song = _dbContext.Songs.Find(id);
            song.Title = songModelNew.Title;
            song.Artist = songModelNew.Artist;
            song.Album = songModelNew.Album;
            song.Genre = songModelNew.Genre;
            song.IsDraft = songModelNew.IsDraft;
            _dbContext.Entry(song).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public SongModel? GetSongById(int id)
        {
            return _dbContext.Songs.Find(id);
        }
    }
}
