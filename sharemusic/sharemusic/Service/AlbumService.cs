using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sharemusic.Db;
using sharemusic.DTO.Album;
using sharemusic.Interface;
using sharemusic.Models;
using System.Runtime.CompilerServices;

namespace sharemusic.Service
{
    public class AlbumService : IAlbumService
    {
        private readonly MusicDbContext _context;
        private readonly IMapper _mapper;
        public AlbumService(MusicDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AlbumShortModelDTO>> GetAllAlbums()
        {
            var albums = await _context.Albums.ToListAsync();
            return _mapper.Map<List<AlbumShortModelDTO>>(albums);
        }

        public async Task<AlbumModel> GetAlbumBySpotifyId(string spotifyId)
        {
            var album = await _context.Albums.Include(x => x.Songs)
                                    .FirstOrDefaultAsync(x => x.SpotifyId == spotifyId);
            if (album == null)
            {
                throw new KeyNotFoundException($"Album with Spotify ID '{spotifyId}' not found.");
            }
            return album;
        }


    }
}
