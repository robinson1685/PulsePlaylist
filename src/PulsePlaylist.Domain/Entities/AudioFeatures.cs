namespace PulsePlaylist.Domain.Entities;

public class AudioFeatures
{
    public AudioFeatures(
        int bpm,
        float energy,
        float valence,
        string spotifyId)
    {
        if (bpm <= 0)
            throw new ArgumentException("BPM must be greater than 0", nameof(bpm));

        if (energy < 0 || energy > 1)
            throw new ArgumentException("Energy must be between 0 and 1", nameof(energy));

        if (valence < 0 || valence > 1)
            throw new ArgumentException("Valence must be between 0 and 1", nameof(valence));

        if (string.IsNullOrWhiteSpace(spotifyId))
            throw new ArgumentException("SpotifyId cannot be null or empty", nameof(spotifyId));

        Bpm = bpm;
        Energy = energy;
        Valence = valence;
        SpotifyId = spotifyId;
    }

    public int Bpm { get; private set; }
    public float Energy { get; private set; }
    public float Valence { get; private set; }
    public string SpotifyId { get; private set; }
}
