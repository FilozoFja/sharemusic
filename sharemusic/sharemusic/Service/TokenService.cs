using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using sharemusic.Db;
using sharemusic.Interface;
using sharemusic.Models;
using System.Text.Json;

namespace sharemusic.Service
{
    public class TokenService : ITokenService
    {
        private readonly MusicDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public TokenService(MusicDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task ClearTokenAsync()
        {
            var token = await _dbContext.SpotifyTokens.FirstOrDefaultAsync();
            if (token != null)
            {
                _dbContext.SpotifyTokens.Remove(token);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<SpotifyTokenRequestModel?> GetAccessTokenAsync()
        {
            return await _dbContext.SpotifyTokens.FirstOrDefaultAsync();
        }

        public async Task<bool> IsTokenExpiredAsync()
        {
            var token = await _dbContext.SpotifyTokens.FirstOrDefaultAsync();

            if (token == null)
                return true;

            return DateTime.UtcNow >= token.ExpiresAt;
        }

        public async Task<bool> IsTokenValidAsync()
        {
            var token = await _dbContext.SpotifyTokens.FirstOrDefaultAsync();

            if (token == null || string.IsNullOrEmpty(token.AccessToken))
            {
                return false;
            }

            return DateTime.UtcNow < token.ExpiresAt;
        }

        public async Task SaveToken(SpotifyTokenRequestModel spotifyTokenRequestModel)
        {
            await ClearTokenAsync();
            await _dbContext.SpotifyTokens.AddAsync(spotifyTokenRequestModel);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateTokenAsync(SpotifyTokenRequestModel spotifyNewToken)
        {
            var oldToken = await _dbContext.SpotifyTokens.FirstOrDefaultAsync();
            if (oldToken != null)
            {
                oldToken.AccessToken = spotifyNewToken.AccessToken;
                oldToken.RefreshToken = spotifyNewToken.RefreshToken;
                oldToken.ExpiresIn = spotifyNewToken.ExpiresIn;
                oldToken.TokenType = spotifyNewToken.TokenType;
                oldToken.Scope = spotifyNewToken.Scope;

                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("No existing token found to update.");
            }
        }
    }
}
