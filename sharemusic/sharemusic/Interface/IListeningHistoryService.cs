using sharemusic.DTO.ListeningHistory;
using sharemusic.Models;

namespace sharemusic.Interface
{
    public interface IListeningHistoryService
    {
        public Task<ListeningHistoryModelDTO> AddToHistory(string spotifySongId, int playlistId);
        public Task<List<ListeningHistoryModelDTO>> SearchByDate(DateTime start, DateTime? end);
        public Task<List<ListeningHistoryModelDTO>> GetTopListened(int top);
        public Task<List<ListeningHistoryModelDTO>> GetTopListenedArtists(int top);
        public Task<List<ListeningHistoryModelDTO>> GetTopListenedGenres(int top);
        
    }
}
