namespace PulsePlaylist.Domain.Entities;

public class Track
{
    public Track(string spotifyId, string name, string artist, int durationMs)
    {
        if (string.IsNullOrWhiteSpace(spotifyId))
            throw new ArgumentException("SpotifyId cannot be null or empty", nameof(spotifyId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty", nameof(name));

        if (string.IsNullOrWhiteSpace(artist))
            throw new ArgumentException("Artist cannot be null or empty", nameof(artist));

        if (durationMs <= 0)
            throw new ArgumentException("Duration must be greater than 0", nameof(durationMs));

        SpotifyId = spotifyId;
        Name = name;
        Artist = artist;
        DurationMs = durationMs;
    }

    public string SpotifyId { get; private set; }
    public string Name { get; private set; }
    public string Artist { get; private set; }
    public int DurationMs { get; private set; }
    public AudioFeatures? AudioFeatures { get; private set; }
}
