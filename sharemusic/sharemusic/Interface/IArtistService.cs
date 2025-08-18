using sharemusic.DTO.ArtistModel;
using sharemusic.Models;
using sharemusic.DTOs;

namespace sharemusic.Interface;

public interface IArtistService
{
    public Task<List<ArtistShortModelDTO>> GetAllSongsFromArtistAsync(string id);
    public Task<List<ArtistShortModelDTO?>> GetArtistsByNameAsync(string name, int? take=null);
    public Task<ArtistModelDTO> GetArtistBySpotifyIdAsync(string id);
    public Task<ArtistModel> UpdateArtistAsync(string id, ArtistModelDTO artistDto);
}