namespace sharemusic.DTO
{
    public class SpotifyPlaylistsResponse
    {
        public string? Href { get; set; }
        public int Limit { get; set; }
        public string? Next { get; set; }
        public int Offset { get; set; }
        public string? Previous { get; set; }
        public int Total { get; set; }
        public List<SpotifyPlaylistItem> Items { get; set; } = new List<SpotifyPlaylistItem>();
    }

    public class SpotifyPlaylistItem
    {
        public bool Collaborative { get; set; }
        public string? Description { get; set; }
        public ExternalUrls? External_urls { get; set; }
        public string? Href { get; set; }
        public string Id { get; set; } = string.Empty;
        public List<SpotifyImage>? Images { get; set; }
        public string Name { get; set; } = string.Empty;
        public SpotifyOwner? Owner { get; set; }
        public bool Public { get; set; }
        public string? Snapshot_id { get; set; }
        public SpotifyTracks? Tracks { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Uri { get; set; } = string.Empty;
    }

    public class SpotifyImage
    {
        public string? Url { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
    }

    public class ExternalUrls
    {
        public string? Spotify { get; set; }
    }

    public class SpotifyOwner
    {
        public ExternalUrls? External_urls { get; set; }
        public SpotifyFollowers? Followers { get; set; }
        public string? Href { get; set; }
        public string Id { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Uri { get; set; } = string.Empty;
        public string? Display_name { get; set; }
    }

    public class SpotifyFollowers
    {
        public string? Href { get; set; }
        public int Total { get; set; }
    }

    public class SpotifyTracks
    {
        public string? Href { get; set; }
        public int Total { get; set; }
    }
}