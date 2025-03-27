using FluentAssertions;
using PulsePlaylist.Domain.Entities;
using Xunit;

namespace PulsePlaylist.Domain.Tests.Entities;

public class AudioFeaturesTests
{
    [Fact]
    public void AudioFeatures_WithValidParameters_ShouldCreateAudioFeatures()
    {
        // Arrange
        var bpm = 128;
        var energy = 0.8f;
        var valence = 0.6f;
        var spotifyId = "spotify:track:123456";

        // Act
        var features = new AudioFeatures(bpm, energy, valence, spotifyId);

        // Assert
        features.Bpm.Should().Be(bpm);
        features.Energy.Should().Be(energy);
        features.Valence.Should().Be(valence);
        features.SpotifyId.Should().Be(spotifyId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-120)]
    public void AudioFeatures_WithInvalidBpm_ShouldThrowArgumentException(int invalidBpm)
    {
        // Arrange
        var energy = 0.8f;
        var valence = 0.6f;
        var spotifyId = "spotify:track:123456";

        // Act & Assert
        var act = () => new AudioFeatures(invalidBpm, energy, valence, spotifyId);
        act.Should().Throw<ArgumentException>()
           .WithMessage("BPM must be greater than 0*");
    }

    [Theory]
    [InlineData(-0.1f)]
    [InlineData(1.1f)]
    [InlineData(2.0f)]
    public void AudioFeatures_WithInvalidEnergy_ShouldThrowArgumentException(float invalidEnergy)
    {
        // Arrange
        var bpm = 128;
        var valence = 0.6f;
        var spotifyId = "spotify:track:123456";

        // Act & Assert
        var act = () => new AudioFeatures(bpm, invalidEnergy, valence, spotifyId);
        act.Should().Throw<ArgumentException>()
           .WithMessage("Energy must be between 0 and 1*");
    }

    [Theory]
    [InlineData(-0.1f)]
    [InlineData(1.1f)]
    [InlineData(2.0f)]
    public void AudioFeatures_WithInvalidValence_ShouldThrowArgumentException(float invalidValence)
    {
        // Arrange
        var bpm = 128;
        var energy = 0.8f;
        var spotifyId = "spotify:track:123456";

        // Act & Assert
        var act = () => new AudioFeatures(bpm, energy, invalidValence, spotifyId);
        act.Should().Throw<ArgumentException>()
           .WithMessage("Valence must be between 0 and 1*");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void AudioFeatures_WithInvalidSpotifyId_ShouldThrowArgumentException(string invalidSpotifyId)
    {
        // Arrange
        var bpm = 128;
        var energy = 0.8f;
        var valence = 0.6f;

        // Act & Assert
        var act = () => new AudioFeatures(bpm, energy, valence, invalidSpotifyId);
        act.Should().Throw<ArgumentException>()
           .WithMessage("SpotifyId cannot be null or empty*");
    }
}
