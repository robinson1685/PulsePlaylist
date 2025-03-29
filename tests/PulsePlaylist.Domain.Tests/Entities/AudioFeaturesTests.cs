using FluentAssertions;
using PulsePlaylist.Domain.Entities;
using Xunit;

namespace PulsePlaylist.Domain.Tests.Entities;

public class AudioFeaturesTests
{
    private readonly string _id = "af-1";
    private readonly string _trackId = "track-1";
    private readonly int _bpm = 128;
    private readonly float _energy = 0.8f;
    private readonly float _valence = 0.6f;
    private readonly string _spotifyId = "spotify:audio-features:123";

    [Fact]
    public void Constructor_WithValidParameters_CreatesAudioFeatures()
    {
        // Act
        var audioFeatures = new AudioFeatures(
            _id,
            _trackId,
            _bpm,
            _energy,
            _valence,
            _spotifyId);

        // Assert
        audioFeatures.Id.Should().Be(_id);
        audioFeatures.TrackId.Should().Be(_trackId);
        audioFeatures.Bpm.Should().Be(_bpm);
        audioFeatures.Energy.Should().Be(_energy);
        audioFeatures.Valence.Should().Be(_valence);
        audioFeatures.SpotifyId.Should().Be(_spotifyId);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void Constructor_WithInvalidId_ThrowsArgumentException(string invalidId)
    {
        // Act & Assert
        var act = () => new AudioFeatures(
            invalidId,
            _trackId,
            _bpm,
            _energy,
            _valence,
            _spotifyId);

        act.Should().Throw<ArgumentException>()
            .WithMessage("ID cannot be empty*");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void Constructor_WithInvalidTrackId_ThrowsArgumentException(string invalidTrackId)
    {
        // Act & Assert
        var act = () => new AudioFeatures(
            _id,
            invalidTrackId,
            _bpm,
            _energy,
            _valence,
            _spotifyId);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Track ID cannot be empty*");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Constructor_WithInvalidBpm_ThrowsArgumentException(int invalidBpm)
    {
        // Act & Assert
        var act = () => new AudioFeatures(
            _id,
            _trackId,
            invalidBpm,
            _energy,
            _valence,
            _spotifyId);

        act.Should().Throw<ArgumentException>()
            .WithMessage("BPM must be greater than 0*");
    }

    [Theory]
    [InlineData(-0.1f)]
    [InlineData(1.1f)]
    [InlineData(2.0f)]
    public void Constructor_WithInvalidEnergy_ThrowsArgumentException(float invalidEnergy)
    {
        // Act & Assert
        var act = () => new AudioFeatures(
            _id,
            _trackId,
            _bpm,
            invalidEnergy,
            _valence,
            _spotifyId);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Energy must be between 0 and 1*");
    }

    [Theory]
    [InlineData(-0.1f)]
    [InlineData(1.1f)]
    [InlineData(2.0f)]
    public void Constructor_WithInvalidValence_ThrowsArgumentException(float invalidValence)
    {
        // Act & Assert
        var act = () => new AudioFeatures(
            _id,
            _trackId,
            _bpm,
            _energy,
            invalidValence,
            _spotifyId);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Valence must be between 0 and 1*");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void Constructor_WithInvalidSpotifyId_ThrowsArgumentException(string invalidSpotifyId)
    {
        // Act & Assert
        var act = () => new AudioFeatures(
            _id,
            _trackId,
            _bpm,
            _energy,
            _valence,
            invalidSpotifyId);

        act.Should().Throw<ArgumentException>()
            .WithMessage("SpotifyId cannot be null or empty*");
    }

    [Theory]
    [InlineData(0.0f)]
    [InlineData(0.5f)]
    [InlineData(1.0f)]
    public void Constructor_WithValidEnergyValues_CreatesAudioFeatures(float validEnergy)
    {
        // Act
        var audioFeatures = new AudioFeatures(
            _id,
            _trackId,
            _bpm,
            validEnergy,
            _valence,
            _spotifyId);

        // Assert
        audioFeatures.Energy.Should().Be(validEnergy);
    }

    [Theory]
    [InlineData(0.0f)]
    [InlineData(0.5f)]
    [InlineData(1.0f)]
    public void Constructor_WithValidValenceValues_CreatesAudioFeatures(float validValence)
    {
        // Act
        var audioFeatures = new AudioFeatures(
            _id,
            _trackId,
            _bpm,
            _energy,
            validValence,
            _spotifyId);

        // Assert
        audioFeatures.Valence.Should().Be(validValence);
    }
}
