using System.ComponentModel.DataAnnotations;

namespace sharemusic.DTO
{
    public class SpotifyTokenRequestModelDTO
    {
        [Required(ErrorMessage = "Access token jest wymagany")]
        [StringLength(2000, MinimumLength = 50, ErrorMessage = "Access token ma nieprawidłowy format")]
        [RegularExpression(@"^[A-Za-z0-9_-]+$", ErrorMessage = "Access token zawiera nieprawidłowe znaki")]
        public required string AccessToken { get; set; }

        [Required(ErrorMessage = "ExpiresIn jest wymagane")]
        [Range(1, 7200, ErrorMessage = "ExpiresIn musi być między 1 a 7200 sekund")]
        public int ExpiresIn { get; set; }

        [StringLength(50, ErrorMessage = "State nie może być dłuższy niż 50 znaków")]
        public string? State { get; set; }

        [StringLength(200, ErrorMessage = "Scope nie może być dłuższy niż 200 znaków")]
        public string? Scope { get; set; }

        [StringLength(500, ErrorMessage = "RefreshToken nie może być dłuższy niż 500 znaków")]
        public string? RefreshToken { get; set; }
    }
}