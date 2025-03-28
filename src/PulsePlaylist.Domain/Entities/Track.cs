using PulsePlaylist.Domain.Common;

namespace PulsePlaylist.Domain.Entities;

public class Track : BaseAuditableEntity
{
    public string SpotifyId { get; private set; }
    public string Name { get; private set; }
    public string Artist { get; private set; }
    public int DurationMs { get; private set; }
    public Uri? SpotifyUri { get; private set; }

    // Audio Features from Spotify
    public double Tempo { get; private set; }           // BPM
    public double Energy { get; private set; }          // 0.0 to 1.0
    public double Danceability { get; private set; }    // 0.0 to 1.0
    public double Valence { get; private set; }         // 0.0 to 1.0 (musical positiveness)
    public double Instrumentalness { get; private set; } // 0.0 to 1.0
    public int Key { get; private set; }                // -1 to 11 (musical key)
    public int Mode { get; private set; }               // 0 = minor, 1 = major
    public double Loudness { get; private set; }        // Overall loudness in dB

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
        Id = id;
        SpotifyId = spotifyId;
        Name = name;
        Artist = artist;
        DurationMs = durationMs;
        SpotifyUri = spotifyUri;
    }

    public void UpdateAudioFeatures(
        double tempo,
        double energy,
        double danceability,
        double valence,
        double instrumentalness,
        int key,
        int mode,
        double loudness)
    {
        if (tempo < 0) throw new ArgumentException("Tempo cannot be negative", nameof(tempo));
        if (energy < 0 || energy > 1) throw new ArgumentException("Energy must be between 0 and 1", nameof(energy));
        if (danceability < 0 || danceability > 1) throw new ArgumentException("Danceability must be between 0 and 1", nameof(danceability));
        if (valence < 0 || valence > 1) throw new ArgumentException("Valence must be between 0 and 1", nameof(valence));
        if (instrumentalness < 0 || instrumentalness > 1) throw new ArgumentException("Instrumentalness must be between 0 and 1", nameof(instrumentalness));
        if (key < -1 || key > 11) throw new ArgumentException("Key must be between -1 and 11", nameof(key));
        if (mode != 0 && mode != 1) throw new ArgumentException("Mode must be 0 (minor) or 1 (major)", nameof(mode));

        Tempo = tempo;
        Energy = energy;
        Danceability = danceability;
        Valence = valence;
        Instrumentalness = instrumentalness;
        Key = key;
        Mode = mode;
        Loudness = loudness;
    }

    public void UpdateMetadata(string name, string artist, int durationMs, Uri? spotifyUri = null)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name cannot be null or empty", nameof(name));
        if (string.IsNullOrEmpty(artist)) throw new ArgumentException("Artist cannot be null or empty", nameof(artist));
        if (durationMs <= 0) throw new ArgumentException("Duration must be positive", nameof(durationMs));

        Name = name;
        Artist = artist;
        DurationMs = durationMs;
        SpotifyUri = spotifyUri;
    }
}
