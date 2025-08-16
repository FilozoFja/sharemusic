using sharemusic.Db;
using sharemusic.DTO.SongModel;
using sharemusic.DTO.ArtistModel;
using sharemusic.Interface;
using sharemusic.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using sharemusic.DTOs;

namespace sharemusic.Service;

public class ArtistService : IArtistService
{
    private readonly MusicDbContext _musicDbContext;
    private readonly IMapper _mapper;

    public ArtistService(MusicDbContext musicDbContext, IMapper mapper)
    {
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

    public async Task<List<ArtistShortModelDTO>> GetArtistsByNameAsync(string name, int? take=null)
    {
        if(string.IsNullOrEmpty(name)){
            throw new ArgumentException("Search term cannot be empty.", nameof(name));
        }

        var query = _musicDbContext.Artists
            .Where(x => !string.IsNullOrEmpty(x.Name) && EF.Functions.Like(x.Name.ToLower(), $"%{name.ToLower()}%"))
            .ProjectTo<ArtistShortModelDTO>(_mapper.ConfigurationProvider);
            
        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }

        return await query.ToListAsync();
    }

    public async Task AddArtistAsync(ArtistModelDTO artist)
    {
        if (artist == null)
        {
            throw new ArgumentNullException(nameof(artist));
        }
        _musicDbContext.Artists.Add(_mapper.Map<ArtistModel>(artist));
        await _musicDbContext.SaveChangesAsync();
    }

    public async Task DeleteArtistAsync(string id)
    {
        var artist = await _musicDbContext.Artists.FindAsync(id);
        if (artist == null)
        {
            throw new KeyNotFoundException("Artist not found.");
        }
        _musicDbContext.Artists.Remove(artist);
        await _musicDbContext.SaveChangesAsync();
    }

    public async Task<ArtistModelDTO> UpdateArtistAsync(string id, ArtistModelDTO artistDto)
    {
        if (artistDto == null)
        {
            throw new ArgumentNullException(nameof(artistDto));
        }

        var artist = await _musicDbContext.Artists.FindAsync(id);
        if (artist == null)
        {
            throw new ArgumentException("Artist not found.", nameof(id));
        }

        artist.Name = artistDto.Name;
        artist.ImageUrl = artistDto.ImageUrl;
        artist.Genres = artistDto.Genres;

        _musicDbContext.Artists.Update(artist);
        await _musicDbContext.SaveChangesAsync();

        return _mapper.Map<ArtistModelDTO>(artist);
    }
}