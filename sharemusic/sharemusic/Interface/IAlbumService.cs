using sharemusic.DTO.Album;
using sharemusic.Models;

namespace sharemusic.Interface
{
    public interface IAlbumService
    {
        public Task<List<AlbumShortModelDTO>> GetAllAlbums();
        public Task<AlbumModel> GetAlbumBySpotifyId(string spotifyId);
    }
}
