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
    public async Task<List<ArtistShortModelDTO>> GetAllSongsFromArtistAsync(string id)
    {
        return await _musicDbContext.Songs.Where(x => x.ArtistSpotifyId == id)
            .ProjectTo<ArtistShortModelDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
    /// <summary>
    /// Get artists by name.
    /// </summary>
    public async Task<List<ArtistShortModelDTO?>> GetArtistsByNameAsync(string name, int? take=null)
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

        return (await query.ToListAsync()).Cast<ArtistShortModelDTO?>().ToList();
    }
    /// <summary>
    /// Updating artist information by spotify ID.
    /// </summary>
    public async Task<ArtistModel> UpdateArtistAsync(string spotifyId, ArtistModelDTO artistDto)
    {
        if (artistDto == null)
        {
            throw new ArgumentNullException(nameof(artistDto));
        }

        var artist = await _musicDbContext.Artists.FindAsync(spotifyId);
        if (artist == null)
        {
            throw new ArgumentException("Artist not found.", nameof(spotifyId));
        }

        artist.Name = artistDto.Name;
        artist.ImageUrl = artistDto.ImageUrl;
        artist.Genres = artistDto.Genres;

        _musicDbContext.Artists.Update(artist);
        await _musicDbContext.SaveChangesAsync();

        return artist;
    }
    /// <summary>
    /// Get artist by Spotify ID.
    /// </summary>
    public async Task<ArtistModelDTO> GetArtistBySpotifyIdAsync(string spotifyId)
    {

       if (string.IsNullOrEmpty(spotifyId))
        {
            throw new ArgumentException("Spotify ID cannot be null or empty.", nameof(spotifyId));
        }
       var artist = await _musicDbContext.Artists
            .Include(a => a.Songs)
            .Where(x => x.SpotifyId == spotifyId)
            .ProjectTo<ArtistModelDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        if (artist == null)
        {
            throw new ArgumentException("Artist doesnt exist.");
        }
        return artist;
    }
}