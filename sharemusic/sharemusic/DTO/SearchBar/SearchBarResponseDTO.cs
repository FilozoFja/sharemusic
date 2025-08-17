using sharemusic.DTO;
using sharemusic.DTO.ArtistModel;
using sharemusic.DTO.PlaylistModel;
using sharemusic.DTO.SongModel;
public class SearchBarResponseDTO
{
    public List<ArtistShortModelDTO> Artist { get; set; }
    public List<PlaylistShortModelDTO> Playlists { get; set; }
    public List<SongShortModelDTO> Songs { get; set; }

}