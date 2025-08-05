using sharemusic.Db;
using sharemusic.DTO;
using sharemusic.Interface;
using sharemusic.Models;
using System.Text.Json;
namespace sharemusic.Service
{
    public class SpotifyService : ISpotifyService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _tokenService;
        private readonly MusicDbContext _musicDbContext;

        public SpotifyService(IHttpClientFactory httpClientFactory, ITokenService tokenService, MusicDbContext musicDbContext)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
            _musicDbContext = musicDbContext;
        }

        public async Task GetPlaylistFromUser(string? accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                if (await _tokenService.IsTokenValidAsync())
                {
                     accessToken = await _tokenService.GetAccessTokenStringAsync();
                }
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync("https://api.spotify.com/v1/me/playlists");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error fetching playlists: {response.ReasonPhrase}");
            }
            var data = await response.Content.ReadAsStringAsync();


            var playlists = JsonSerializer.Deserialize<SpotifyPlaylistsResponse>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (playlists != null && playlists.Items != null)
            {
                foreach (var playlist in playlists.Items)
                {
                    try
                    {

                        var existingPlaylist = _musicDbContext.Playlists.FirstOrDefault(p => p.Name == playlist.Name);

                        if (existingPlaylist == null)
                        {
                            var newPlaylist = new PlaylistModel
                            {
                                SpotifyId = playlist.Id,
                                Name = playlist.Name ?? "Unnamed",
                                Description = playlist.Description ?? "",
                                CoverUrl = playlist.Images?.FirstOrDefault()?.Url,
                                Songs = new List<SongModel>()
                            };
                            _musicDbContext.Playlists.Add(newPlaylist);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error in playlists {playlist.Name}: {ex.Message}");
                    }
                }
                var saved = await _musicDbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Your account is empty or there is unpredictable error.");
            }
        }
    }
}