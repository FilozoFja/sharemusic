using AutoMapper;
using Microsoft.EntityFrameworkCore;
using sharemusic.Db;
using sharemusic.DTO.GenreModel;
using sharemusic.Interface;
using sharemusic.Models;

namespace sharemusic.Service
{
    public class GenreService : IGenreService
    {
        private readonly MusicDbContext _context;
        private readonly IMapper _mapper;
        public GenreService(MusicDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenreModel> GetGenreById(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Genre ID cannot be null or empty.", nameof(id));
            }
            var genre = await _context.Genres
                .Include(a => a.Songs)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (genre == null)
            {
                throw new KeyNotFoundException($"Genre with ID {id} not found.");
            }
            return genre;
        }

        public async Task<List<GenreShortModelDTO>> GetGenreShortAsync()
        {
            var genres = await _context.Genres.ToListAsync();
            if (genres == null || genres.Count == 0)
            {
                return new List<GenreShortModelDTO>();
            }
            return _mapper.Map<List<GenreShortModelDTO>>(genres);
        }
    }
}
