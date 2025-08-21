using Microsoft.EntityFrameworkCore;
using sharemusic.Db;
using sharemusic.Interface;
using sharemusic.Models;
using AutoMapper;
using sharemusic.DTO;

namespace sharemusic.Service
{
    public class TokenService : ITokenService
    {
        private readonly MusicDbContext _dbContext;
        private readonly IMapper _mapper;

        public TokenService(MusicDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task DeleteTokenAsync()
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

        public async Task<string> GetAccessTokenStringAsync()
        {
            var token = await _dbContext.SpotifyTokens.FirstOrDefaultAsync();
            return string.IsNullOrEmpty(token?.AccessToken) ? throw new Exception("Access token is empty") : token.AccessToken;
        }

        public async Task<bool> IsTokenValidAsync()
        {
            var token = await _dbContext.SpotifyTokens.FirstOrDefaultAsync();
            if (token == null || string.IsNullOrEmpty(token.AccessToken) || DateTime.UtcNow > token.ExpiresAt)
            {
                await DeleteTokenAsync();
                return false;
            }

            return true;
        }

        public async Task SaveTokenAsync(SpotifyTokenRequestModelDTO spotifyTokenRequestModel)
        {
            await DeleteTokenAsync();
            var newToken = _mapper.Map<SpotifyTokenRequestModel>(spotifyTokenRequestModel);
            await _dbContext.SpotifyTokens.AddAsync(newToken);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateTokenAsync(SpotifyTokenRequestModelDTO spotifyNewToken)
        {
            var oldToken = await _dbContext.SpotifyTokens.FirstOrDefaultAsync();
            if (oldToken != null)
            {
                _mapper.Map(spotifyNewToken, oldToken);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No existing token found to update.");
            }
        }
    
    }
}
