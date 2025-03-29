using FluentAssertions;
using PulsePlaylist.Domain.Entities;
using Xunit;

namespace PulsePlaylist.Domain.Tests.Entities;

public class TrackTests
{
    [Fact]
    public void Track_WithValidParameters_ShouldCreateTrack()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var spotifyId = "spotify:track:123456";
        var name = "Test Track";
        var artist = "Test Artist";
        var durationMs = 180000; // 3 minutes

        // Act
        var track = new Track(id, spotifyId, name, artist, durationMs);

        // Assert
        track.Id.Should().Be(id);
        track.SpotifyId.Should().Be(spotifyId);
        track.Name.Should().Be(name);
        track.Artist.Should().Be(artist);
        track.DurationMs.Should().Be(durationMs);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void Track_WithInvalidSpotifyId_ShouldThrowArgumentException(string invalidSpotifyId)
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var name = "Test Track";
        var artist = "Test Artist";
        var durationMs = 180000;

        // Act & Assert
        var act = () => new Track(id, invalidSpotifyId, name, artist, durationMs);
        act.Should().Throw<ArgumentException>()
           .WithMessage("SpotifyId cannot be null or empty*");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void Track_WithInvalidName_ShouldThrowArgumentException(string invalidName)
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var spotifyId = "spotify:track:123456";
        var artist = "Test Artist";
        var durationMs = 180000;

        // Act & Assert
        var act = () => new Track(id, spotifyId, invalidName, artist, durationMs);
        act.Should().Throw<ArgumentException>()
           .WithMessage("Name cannot be null or empty*");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void Track_WithInvalidArtist_ShouldThrowArgumentException(string invalidArtist)
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var spotifyId = "spotify:track:123456";
        var name = "Test Track";
        var durationMs = 180000;

        // Act & Assert
        var act = () => new Track(id, spotifyId, name, invalidArtist, durationMs);
        act.Should().Throw<ArgumentException>()
           .WithMessage("Artist cannot be null or empty*");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-1000)]
    public void Track_WithInvalidDuration_ShouldThrowArgumentException(int invalidDurationMs)
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var spotifyId = "spotify:track:123456";
        var name = "Test Track";
        var artist = "Test Artist";

        // Act & Assert
        var act = () => new Track(id, spotifyId, name, artist, invalidDurationMs);
        act.Should().Throw<ArgumentException>()
           .WithMessage("Duration must be greater than 0*");
    }

    [Fact]
    public void Track_WithoutAudioFeatures_ShouldHaveNullAudioFeatures()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var spotifyId = "spotify:track:123456";
        var name = "Test Track";
        var artist = "Test Artist";
        var durationMs = 180000;

        // Act
        var track = new Track(id, spotifyId, name, artist, durationMs);

        // Assert
        track.Features.Should().BeNull();
    }
}
