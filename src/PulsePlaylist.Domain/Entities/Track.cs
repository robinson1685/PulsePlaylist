using PulsePlaylist.Domain.Common;

namespace PulsePlaylist.Domain.Entities;

public class Track : BaseAuditableEntity
{
    public string SpotifyId { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Artist { get; private set; } = string.Empty;
    public int DurationMs { get; private set; }
    public Uri? SpotifyUri { get; private set; }

    public AudioFeatures? Features { get; private set; }

    // EF Core constructor
    private Track() { }

    public Track(
        string id,
        string spotifyId,
        string name,
        string artist,
        int durationMs,
        Uri? spotifyUri = null)
    {
        if (string.IsNullOrWhiteSpace(spotifyId))
            throw new ArgumentException("SpotifyId cannot be null or empty", nameof(spotifyId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty", nameof(name));

        if (string.IsNullOrWhiteSpace(artist))
            throw new ArgumentException("Artist cannot be null or empty", nameof(artist));

        if (durationMs <= 0)
            throw new ArgumentException("Duration must be greater than 0", nameof(durationMs));

        Id = id;
        SpotifyId = spotifyId;
        Name = name;
        Artist = artist;
        DurationMs = durationMs;
        SpotifyUri = spotifyUri;
    }

    public void UpdateMetadata(string name, string artist, int durationMs, Uri? spotifyUri = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name cannot be null or empty", nameof(name));

        if (string.IsNullOrEmpty(artist))
            throw new ArgumentException("Artist cannot be null or empty", nameof(artist));

        if (durationMs <= 0)
            throw new ArgumentException("Duration must be positive", nameof(durationMs));

        Name = name;
        Artist = artist;
        DurationMs = durationMs;
        SpotifyUri = spotifyUri;
    }

    public void SetAudioFeatures(AudioFeatures features)
    {
        ArgumentNullException.ThrowIfNull(features);
        Features = features;
    }
}
