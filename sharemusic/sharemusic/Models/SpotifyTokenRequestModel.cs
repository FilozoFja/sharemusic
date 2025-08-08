using System.ComponentModel.DataAnnotations;

namespace sharemusic.Models
{
    public class SpotifyTokenRequestModel
    {
        [Key]
        [Required]
        public required string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; } = "Bearer";
        public string? State { get; set; }
        public string? Scope { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime ExpiresAt => DateTime.UtcNow.AddSeconds(ExpiresIn);
        public bool HasRefreshToken => !string.IsNullOrEmpty(RefreshToken);
    }
}
