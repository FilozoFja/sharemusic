using sharemusic.Db;
using sharemusic.Interface;
using SpotifyAPI.Web;
using AutoMapper;
using sharemusic.Models;

namespace sharemusic.Service
{
    public class PlaylistService : IPlaylistService
    {
        private readonly MusicDbContext _musicDbContext;
        private readonly IMapper _mapper;

        public PlaylistService(MusicDbContext musicDbContext, IMapper mapper)
        {
            _musicDbContext = musicDbContext;
            _mapper = mapper;
        }
        public async Task AddPlaylist(FullPlaylist playlistToAdd)
        {
            var playlist = _mapper.Map<PlaylistModel>(playlistToAdd);
            await _musicDbContext.Playlists.AddAsync(playlist);
            await _musicDbContext.SaveChangesAsync();
        }
    }
}