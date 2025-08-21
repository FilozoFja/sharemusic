using sharemusic.DTO.ListeningHistory;
using sharemusic.Models;

namespace sharemusic.Interface
{
    public interface IListeningHistoryService
    {
        public Task<ListeningHistoryModelDTO> AddToHistory(string spotifySongId, int playlistId);
    }
}
