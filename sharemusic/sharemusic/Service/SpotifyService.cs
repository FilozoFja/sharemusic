using sharemusic.Db;
using sharemusic.DTO;
using sharemusic.Interface;
using sharemusic.Models;
using SpotifyAPI.Web;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace sharemusic.Service
{
    public class SpotifyService : ISpotifyService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _tokenService;
        private readonly MusicDbContext _musicDbContext;
        private readonly IPlaylistService _playlistService;

        public SpotifyService(IHttpClientFactory httpClientFactory, ITokenService tokenService
                                    , MusicDbContext musicDbContext, IPlaylistService playlistService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
            _musicDbContext = musicDbContext;
            _playlistService = playlistService;
        }

        public async Task DownloadPlaylistFromUser()
        {
            var token = await _tokenService.GetAccessTokenAsync();

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AccessToken);

            await Task.Delay(500);

            var response = await client.GetAsync("https://api.spotify.com/v1/me/playlists");

            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                if (response.Headers.TryGetValues("Retry-After", out var values))
                {
                    var retryAfterSeconds = int.Parse(values.First());
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[Spotify API] Za dużo zapytań. Spróbuj ponownie za {retryAfterSeconds} sekund.");
                    Console.ResetColor();

                    await Task.Delay(retryAfterSeconds * 1000);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[Spotify API] Za dużo zapytań, brak nagłówka Retry-After.");
                    Console.ResetColor();
                    await Task.Delay(60000);
                }
                return;
            }

            if (!response.IsSuccessStatusCode)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error fetching playlists: {response.StatusCode} - {response.ReasonPhrase}");
                Console.ResetColor();
                return;
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
                        var existingPlaylist = _musicDbContext.Playlists.FirstOrDefault(p => p.SpotifyId == playlist.Id);
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

                        await Task.Delay(100);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error in playlist {playlist.Name}: {ex.Message}");
                        Console.ResetColor();
                    }
                }
                await _musicDbContext.SaveChangesAsync();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Your account is empty or there is unpredictable error.");
                Console.ResetColor();
            }
        }

        public async Task DownloadSongsFromUserPlaylist(string playlistId)
        {
            SpotifyClient spotify = await SetSpotifyDefaultRequest();

            await Task.Delay(500);

            var playlistFull = await spotify.Playlists.Get(playlistId);

            // Pobierz playlistę z Include dla Songs
            var playlist = await _musicDbContext.Playlists
                .Include(p => p.Songs)
                .FirstOrDefaultAsync(p => p.SpotifyId == playlistId);

            if (playlist == null)
            {
                await _playlistService.AddPlaylistAsync(playlistFull);

                // Pobierz ponownie po dodaniu
                playlist = await _musicDbContext.Playlists
                    .Include(p => p.Songs)
                    .FirstOrDefaultAsync(p => p.SpotifyId == playlistId);
            }

            // Upewnij się, że kolekcja Songs nie jest null
            if (playlist.Songs == null) playlist.Songs = new List<SongModel>();

            var allTracks = new List<PlaylistTrack<IPlayableItem>>();
            var page = playlistFull.Tracks;

            while (page != null)
            {
                if (page.Items != null)
                {
                    allTracks.AddRange(page.Items);
                }
                if (page.Next == null) break;

                await Task.Delay(300);
                page = await spotify.NextPage(page);
            }

            foreach (var item in allTracks)
            {
                if (item.Track is FullTrack track)
                {
                    var existingSong = _musicDbContext.Songs.FirstOrDefault(s => s.SpotifyId == track.Id);

                    await Task.Delay(200);
                    var mainArtist = await GetOrCreateArtistAsync(track.Artists.First().Id, spotify);

                    if (existingSong == null)
                    {
                        existingSong = new SongModel
                        {
                            SpotifyId = track.Id,
                            Title = track.Name,
                            Artist = string.Join(", ", track.Artists.Select(a => a.Name)),
                            ArtistSpotifyId = track.Artists.FirstOrDefault()?.Id,
                            Album = track.Album?.Name,
                            CoverImageUrl = track.Album?.Images?.FirstOrDefault()?.Url,
                            IsDraft = true,
                            ReleaseDate = DateTime.TryParse(track.Album?.ReleaseDate, out var rd) ? rd : null
                        };
                        _musicDbContext.Songs.Add(existingSong);
                    }

                    if (!playlist.Songs.Any(s => s.SpotifyId == existingSong.SpotifyId))
                    {
                        playlist.Songs.Add(existingSong);
                    }

                    if (!mainArtist.Songs.Any(s => s.SpotifyId == existingSong.SpotifyId))
                    {
                        mainArtist.Songs.Add(existingSong);
                        await _musicDbContext.SaveChangesAsync();
                    }
                }
            }

            await _musicDbContext.SaveChangesAsync();
            Console.WriteLine($"Zakończono przetwarzanie playlisty: {playlist.Name}. Piosenek: {playlist.Songs.Count}");
        }

        public async Task<ArtistModel> GetOrCreateArtistAsync(string artistId, SpotifyClient spotify)
        {
            // Pobierz z Include dla Songs
            var existingArtist = await _musicDbContext.Artists
                .Include(a => a.Songs)
                .FirstOrDefaultAsync(a => a.SpotifyId == artistId);

            if (existingArtist != null)
            {
                return existingArtist;
            }

            var fullArtist = await spotify.Artists.Get(artistId);

            var newArtist = new ArtistModel
            {
                SpotifyId = fullArtist.Id,
                Name = fullArtist.Name,
                ImageUrl = fullArtist.Images?.FirstOrDefault()?.Url,
                Genres = fullArtist.Genres?.ToList() ?? new List<string?>(),
                Songs = new List<SongModel>()
            };

            _musicDbContext.Artists.Add(newArtist);
            await _musicDbContext.SaveChangesAsync();

            Console.WriteLine($"Utworzono nowego artystę: {newArtist.Name}");
            return newArtist;
        }

        public async Task<SpotifyClient> SetSpotifyDefaultRequest()
        {
            var token = await _tokenService.GetAccessTokenAsync();
            var config = SpotifyClientConfig.CreateDefault();
            return new SpotifyClient(config.WithToken(token.AccessToken));
        }
    }
}