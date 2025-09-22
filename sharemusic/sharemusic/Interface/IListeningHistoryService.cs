using sharemusic.DTO.Album;
using sharemusic.DTO.ArtistModel;
using sharemusic.DTO.GenreModel;
using sharemusic.DTO.ListeningHistory;
using sharemusic.DTO.PlaylistModel;
using sharemusic.DTO.SongModel;
using sharemusic.Models;

namespace sharemusic.Interface
{
    public interface IListeningHistoryService
    {
        public Task<ListeningHistoryModelDTO> AddToHistory(string spotifySongId, int playlistId);
        public Task<List<ListeningHistoryModelDTO>> SearchByDate(DateTime start, DateTime? end);
        public Task<List<SongShortModelDTO>> GetTopListenedSong(int top);
        public Task<List<ArtistShortModelDTO>> GetTopListenedArtists(int top);
        public Task<List<GenreShortModelDTO>> GetTopListenedGenres(int top);
        public Task<List<AlbumShortModelDTO>> GetTopListenedAlbums(int top);
        public Task<List<ListeningHistoryModelDTO>> GetRecentListeningHistory(int take);
        Task<List<SongShortModelDTO>> GetLeastPopularSongsByGenre(string genre, int top);
        Task<List<ArtistShortModelDTO>> GetLeastPopularArtistsByGenre(string genre, int top);
        Task<List<PlaylistShortModelDTO>> GetLeastPopularPlaylistsByGenre(string genre, int top);
    }
}
