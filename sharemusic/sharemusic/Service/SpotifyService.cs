using sharemusic.Db;
using sharemusic.DTO;
using sharemusic.Interface;
using sharemusic.Models;
using SpotifyAPI.Web;
using System.Text.Json;
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
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AccessToken);
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

        public async Task DownloadSongsFromUserPlaylist(string playlistId)
        {
            SpotifyClient spotify = await SetSpotifyDefaultRequest();
            var playlistFull = await spotify.Playlists.Get(playlistId);

            var playlist = _musicDbContext.Playlists.FirstOrDefault(p => p.SpotifyId == playlistId);
            if (playlist == null)
            {
                await _playlistService.AddPlaylist(playlistFull);
            }

            var allTracks = new List<PlaylistTrack<IPlayableItem>>();
            var page = playlistFull.Tracks;

            while (page != null)
            {
                if (page.Items != null)
                {
                    allTracks.AddRange(page.Items);
                }
                if (page.Next == null) break;
                page = await spotify.NextPage(page);
            }

            foreach (var item in allTracks)
            {
                if (item.Track is FullTrack track)
                {
                    var existingSong = _musicDbContext.Songs.FirstOrDefault(s => s.SpotifyId == track.Id);
                    var mainArtist = await GetOrCreateArtistAsync(track.Artists.First().Id);

                    if (existingSong == null)
                    {
                        existingSong = new SongModel
                        {
                            SpotifyId = track.Id,
                            Title = track.Name,
                            Artist = string.Join(", ", track.Artists.Select(a => a.Name)),
                            ArtistId = track.Artists.FirstOrDefault()?.Id,
                            Album = track.Album?.Name,
                            CoverImageUrl = track.Album?.Images?.FirstOrDefault()?.Url,
                            IsDraft = true,
                            ReleaseDate = DateTime.TryParse(track.Album?.ReleaseDate, out var rd) ? rd : null
                        };
                        _musicDbContext.Songs.Add(existingSong);
                    }

                    if (playlist != null && playlist.Songs != null && !playlist.Songs.Any(s => s.SpotifyId == existingSong.SpotifyId))
                    {
                        playlist.Songs.Add(existingSong);
                    }
                    if (!mainArtist.Songs.Any(s => s.SpotifyId == existingSong.SpotifyId))
                    {
                        mainArtist.Songs.Add(existingSong);
                    }
                }
            }

            await _musicDbContext.SaveChangesAsync();
        }

        public async Task<ArtistModel> GetOrCreateArtistAsync(string artistId)
        {
            var existingArtist = _musicDbContext.Artists.FirstOrDefault(a => a.SpotifyId == artistId);
            if (existingArtist != null)
            {
                return existingArtist;
            }

            var spotify = await SetSpotifyDefaultRequest();
            var fullArtist = await spotify.Artists.Get(artistId);

            var newArtist = new ArtistModel
            {
                Id = Guid.NewGuid().ToString(),
                SpotifyId = fullArtist.Id,
                Name = fullArtist.Name,
                ImageUrl = fullArtist.Images?.FirstOrDefault()?.Url,
                Genres = fullArtist.Genres?.ToList() ?? new List<string?>(),
                Songs = new List<SongModel>()
            };

            _musicDbContext.Artists.Add(newArtist);
            await _musicDbContext.SaveChangesAsync();

            return newArtist;
        }

        public async Task<SpotifyClient> SetSpotifyDefaultRequest()
        {
            var token = await _tokenService.GetAccessTokenAsync();
            var config = SpotifyClientConfig.CreateDefault();
            return new SpotifyClient(config.WithToken(token.AccessToken)); ;

        }

        
    }  
}