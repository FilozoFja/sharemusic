using sharemusic.DTO.GenreModel;
using sharemusic.Models;

namespace sharemusic.Interface;
public interface IGenreService
{
    public Task<List<GenreShortModelDTO>> GetGenreShortAsync();
    public Task<GenreModel> GetGenreById(string id);
}
