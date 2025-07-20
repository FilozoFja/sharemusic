using System.ComponentModel.DataAnnotations;

namespace sharemusic.DTO
{
    public class SongModelDTO
    {
        [Required(ErrorMessage = "Tytuł piosenki jest wymagany")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Tytuł musi mieć od 1 do 200 znaków")]
        public required string Title { get; set; }

        [StringLength(100, ErrorMessage = "Nazwa artysty nie może być dłuższa niż 100 znaków")]
        public string? Artist { get; set; }

        [StringLength(100, ErrorMessage = "Nazwa albumu nie może być dłuższa niż 100 znaków")]
        public string? Album { get; set; }

        [StringLength(50, ErrorMessage = "Gatunek nie może być dłuższy niż 50 znaków")]
        public string? Genre { get; set; }

        [Url(ErrorMessage = "CoverImageUrl musi być prawidłowym URL")]
        [StringLength(500, ErrorMessage = "URL okładki nie może być dłuższy niż 500 znaków")]
        public string? CoverImageUrl { get; set; }

        [Url(ErrorMessage = "SongUrl musi być prawidłowym URL")]
        [StringLength(500, ErrorMessage = "URL piosenki nie może być dłuższy niż 500 znaków")]
        public string? SongUrl { get; set; }

        public bool IsDraft { get; set; } = true;
    }
}