using System.ComponentModel.DataAnnotations;

namespace sharemusic.Models
{
    public class SpotifyTokenRequestModel
    {
        public int Id { get; set; }
        public required string AccessToken { get; set; }
        public string TokenType { get; set; } = "Bearer";
        public string? State { get; set; }
        public string? Scope { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt => AddedAt.AddSeconds(3600); 
        public bool HasRefreshToken => !string.IsNullOrEmpty(RefreshToken);
    }
}
