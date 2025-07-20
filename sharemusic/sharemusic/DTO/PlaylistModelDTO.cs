using System.ComponentModel.DataAnnotations;

namespace sharemusic.DTO
{
    public class PlaylistModelDTO
    {
        [Required(ErrorMessage = "Nazwa playlisty jest wymagana")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nazwa musi mieć od 1 do 100 znaków")]
        public required string Name { get; set; }

        [StringLength(500, ErrorMessage = "Opis nie może być dłuższy niż 500 znaków")]
        public string? Description { get; set; }

        [Url(ErrorMessage = "CoverUrl musi być prawidłowym URL")]
        [StringLength(500, ErrorMessage = "URL okładki nie może być dłuższy niż 500 znaków")]
        public string? CoverUrl { get; set; }

        public bool IsPublic { get; set; } = true;


        public List<int>? SongIds { get; set; }
    }
}