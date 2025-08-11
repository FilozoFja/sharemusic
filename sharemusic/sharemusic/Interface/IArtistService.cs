using sharemusic.DTO.ArtistModel;
using sharemusic.Models;
using sharemusic.DTOs;

namespace sharemusic.Interface;

public interface IArtistService
{
    public Task<ArtistModel?> GetArtistAsync(string id);
    public Task<List<ArtistShortModelDTO>> GetAllSongsFromArtistAsync(string id);
    public Task<List<ArtistShortModelDTO?>> GetArtistsByNameAsync(string name);
    
    public Task AddArtistAsync(ArtistModelDTO artist);
    public Task DeleteArtistAsync(string id);
    public Task<ArtistModelDTO> UpdateArtistAsync(string id, ArtistModelDTO artistDto);
}