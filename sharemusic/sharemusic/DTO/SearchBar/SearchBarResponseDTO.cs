using sharemusic.DTO;
using sharemusic.DTO.ArtistModel;
using sharemusic.DTO.PlaylistModel;
using sharemusic.DTO.SongModel;
public class SearchBarResponseDTO
{
    public List<ArtistShortModelDTO> Artist;
    public List<PlaylistShortModelDTO> Playlists;
    public List<SongShortModelDTO> Songs;

}