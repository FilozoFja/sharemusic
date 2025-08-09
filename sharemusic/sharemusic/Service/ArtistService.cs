using sharemusic.Db;
using sharemusic.DTO.SongModel;
using sharemusic.DTO.ArtistModel;
using sharemusic.Interface;
using sharemusic.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace sharemusic.Service;

public class ArtistService : IArtistService
{
    private readonly MusicDbContext _musicDbContext;
    private readonly IMapper _mapper;

    public ArtistService(MusicDbContext musicDbContext, IMapper mapper) {
        _musicDbContext = musicDbContext;
        _mapper = mapper;
    }
    public async Task<ArtistModel?> GetArtistAsync(string id)
    {
        return await _musicDbContext.Artists.FindAsync(id);
    }

    public async Task<List<ArtistShortModelDTO>> GetAllSongsFromArtistAsync(string id)
    {
        return await _musicDbContext.Songs.Where(x => x.ArtistId == id)
            .ProjectTo<ArtistShortModelDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<List<ArtistShortModelDTO?>> GetArtistByNameAsync(string name)
    {
        var artists = await _musicDbContext.Artists
            .Where(x => x.Name != null && x.Name.Contains(name))
            .ProjectTo<ArtistShortModelDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
        if (artists == null || artists.Count == 0)
        {
            return [];
        }
        return artists.Cast<ArtistShortModelDTO?>().ToList();
    }
}