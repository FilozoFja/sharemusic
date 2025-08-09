using sharemusic.DTO.ArtistModel;
using sharemusic.Models;

namespace sharemusic.Interface;

public interface IArtistService
{
    public Task<ArtistModel?> GetArtistAsync(string id);
    public Task<List<ArtistShortModelDTO>> GetAllSongsFromArtistAsync(string id);
    public Task<List<ArtistShortModelDTO?>> GetArtistByNameAsync(string name);
}