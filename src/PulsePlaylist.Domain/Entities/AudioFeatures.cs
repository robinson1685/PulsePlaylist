using PulsePlaylist.Domain.Common;

namespace PulsePlaylist.Domain.Entities;

public class AudioFeatures
{
    public string Id { get; private set; }
    public string TrackId { get; private set; }
    public Track Track { get; private set; }
    public int Bpm { get; private set; }
    public float Energy { get; private set; }
    public float Valence { get; private set; }
    public string SpotifyId { get; private set; }

    // EF Core constructor
    private AudioFeatures() { }

    public AudioFeatures(
        string id,
        string trackId,
        int bpm,
        float energy,
        float valence,
        string spotifyId)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("ID cannot be empty", nameof(id));

        if (string.IsNullOrWhiteSpace(trackId))
            throw new ArgumentException("Track ID cannot be empty", nameof(trackId));

        if (bpm <= 0)
            throw new ArgumentException("BPM must be greater than 0", nameof(bpm));

        if (energy < 0 || energy > 1)
            throw new ArgumentException("Energy must be between 0 and 1", nameof(energy));

        if (valence < 0 || valence > 1)
            throw new ArgumentException("Valence must be between 0 and 1", nameof(valence));

        if (string.IsNullOrWhiteSpace(spotifyId))
            throw new ArgumentException("SpotifyId cannot be null or empty", nameof(spotifyId));

        Id = id;
        TrackId = trackId;
        Bpm = bpm;
        Energy = energy;
        Valence = valence;
        SpotifyId = spotifyId;
    }
}
