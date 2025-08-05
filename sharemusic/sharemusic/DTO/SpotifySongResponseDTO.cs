public class SpotifySongsResponse
{
    public List<PlaylistTrackItem> items { get; set; }
}

public class PlaylistTrackItem
{
    public TrackItem track { get; set; }
}

public class TrackItem
{
    public string id { get; set; }
    public string name { get; set; }
    public List<Artist> artists { get; set; }
    public Album album { get; set; }
    public ExternalUrls external_urls { get; set; }
}

public class Artist
{
    public string name { get; set; }
}

public class Album
{
    public string name { get; set; }
    public List<Image> images { get; set; }
}

public class ExternalUrls
{
    public string spotify { get; set; }
}

public class Image
{
    public string url { get; set; }
}
