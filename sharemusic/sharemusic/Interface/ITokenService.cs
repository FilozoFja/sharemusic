using sharemusic.Models;

namespace sharemusic.Interface
{
    public interface ITokenService
    {
        Task SaveToken(SpotifyTokenRequestModel spotifyTokenRequestModel);
        Task<SpotifyTokenRequestModel?> GetAccessTokenAsync();
        Task UpdateTokenAsync(SpotifyTokenRequestModel spotifyTokenRequestModel);


        Task<bool> IsTokenValidAsync();
        Task<bool> IsTokenExpiredAsync();
        Task ClearTokenAsync();
    }
}
