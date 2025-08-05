using sharemusic.Models;

namespace sharemusic.Interface
{
    public interface ITokenService
    {
        Task SaveTokenAsync(SpotifyTokenRequestModel spotifyTokenRequestModel);
        Task<SpotifyTokenRequestModel?> GetAccessTokenAsync();
        Task UpdateTokenAsync(SpotifyTokenRequestModel spotifyTokenRequestModel);
        Task<string> GetAccessTokenStringAsync();


        Task<bool> IsTokenValidAsync();
        Task ClearTokenAsync();
    }
}
