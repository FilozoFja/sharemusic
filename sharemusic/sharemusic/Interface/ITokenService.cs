using sharemusic.DTO;
using sharemusic.Models;

namespace sharemusic.Interface
{
    public interface ITokenService
    {
        Task SaveTokenAsync(SpotifyTokenRequestModelDTO spotifyTokenRequestModel);
        Task<SpotifyTokenRequestModel?> GetAccessTokenAsync();
        Task UpdateTokenAsync(SpotifyTokenRequestModelDTO spotifyTokenRequestModel);
        Task<string> GetAccessTokenStringAsync();


        Task<bool> IsTokenValidAsync();
        Task DeleteTokenAsync();
    }
}
