using FluentAssertions;
using PulsePlaylist.Domain.ValueObjects;
using Xunit;

namespace PulsePlaylist.Domain.Tests.ValueObjects;

public class PlaylistSettingsTests
{
    private readonly int _minTempo = 120;
    private readonly int _maxTempo = 160;
    private readonly Dictionary<string, decimal> _genreWeights;
    private readonly decimal _discoveryLevel = 0.3m;
    private readonly int _recentTrackThreshold = 120;

    public PlaylistSettingsTests()
    {
        _genreWeights = new Dictionary<string, decimal>
        {
            { "pop", 1.0m },
            { "rock", 0.8m }
        };
    }

    [Fact]
    public void Create_WithValidParameters_CreatesPlaylistSettings()
    {
        // Act
        var settings = PlaylistSettings.Create(
            _minTempo,
            _maxTempo,
            _genreWeights,
            _discoveryLevel,
            _recentTrackThreshold);

        // Assert
        settings.MinTempo.Should().Be(_minTempo);
        settings.MaxTempo.Should().Be(_maxTempo);
        settings.GenreWeights.Should().BeEquivalentTo(_genreWeights);
        settings.DiscoveryLevel.Should().Be(_discoveryLevel);
        settings.RecentTrackThreshold.Should().Be(_recentTrackThreshold);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(39)]
    public void Create_WithInvalidMinTempo_ThrowsException(int invalidMinTempo)
    {
        // Act & Assert
        var act = () => PlaylistSettings.Create(
            invalidMinTempo,
            _maxTempo,
            _genreWeights,
            _discoveryLevel,
            _recentTrackThreshold);

        act.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("*minTempo*");
    }

    [Theory]
    [InlineData(201)]
    [InlineData(300)]
    public void Create_WithInvalidMaxTempo_ThrowsException(int invalidMaxTempo)
    {
        // Act & Assert
        var act = () => PlaylistSettings.Create(
            _minTempo,
            invalidMaxTempo,
            _genreWeights,
            _discoveryLevel,
            _recentTrackThreshold);

        act.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("*maxTempo*");
    }

    [Fact]
    public void Create_WithMaxTempoLessThanMinTempo_ThrowsException()
    {
        // Arrange
        var invalidMaxTempo = _minTempo - 1;

        // Act & Assert
        var act = () => PlaylistSettings.Create(
            _minTempo,
            invalidMaxTempo,
            _genreWeights,
            _discoveryLevel,
            _recentTrackThreshold);

        act.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("*maxTempo*");
    }

    [Fact]
    public void Create_WithNullGenreWeights_ThrowsException()
    {
        // Act & Assert
        var act = () => PlaylistSettings.Create(
            _minTempo,
            _maxTempo,
            null!,
            _discoveryLevel,
            _recentTrackThreshold);

        act.Should().Throw<ArgumentNullException>()
            .WithMessage("*genreWeights*");
    }

    [Fact]
    public void Create_WithEmptyGenreWeights_ThrowsException()
    {
        // Act & Assert
        var act = () => PlaylistSettings.Create(
            _minTempo,
            _maxTempo,
            new Dictionary<string, decimal>(),
            _discoveryLevel,
            _recentTrackThreshold);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Must have at least one genre weight*");
    }

    [Theory]
    [InlineData(-0.1)]
    [InlineData(1.1)]
    public void Create_WithInvalidDiscoveryLevel_ThrowsException(decimal invalidDiscoveryLevel)
    {
        // Act & Assert
        var act = () => PlaylistSettings.Create(
            _minTempo,
            _maxTempo,
            _genreWeights,
            invalidDiscoveryLevel,
            _recentTrackThreshold);

        act.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("*discoveryLevel*");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-60)]
    public void Create_WithInvalidRecentTrackThreshold_ThrowsException(int invalidThreshold)
    {
        // Act & Assert
        var act = () => PlaylistSettings.Create(
            _minTempo,
            _maxTempo,
            _genreWeights,
            _discoveryLevel,
            invalidThreshold);

        act.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("*recentTrackThreshold*");
    }

    [Fact]
    public void CreateDefault_ReturnsValidSettings()
    {
        // Act
        var settings = PlaylistSettings.CreateDefault();

        // Assert
        settings.MinTempo.Should().Be(120);
        settings.MaxTempo.Should().Be(160);
        settings.GenreWeights.Should().HaveCount(3);
        settings.GenreWeights["pop"].Should().Be(1.0m);
        settings.GenreWeights["rock"].Should().Be(1.0m);
        settings.GenreWeights["electronic"].Should().Be(1.0m);
        settings.DiscoveryLevel.Should().Be(0.3m);
        settings.RecentTrackThreshold.Should().Be(120);
    }

    [Fact]
    public void GenreWeights_PersistsCorrectly()
    {
        // Arrange
        var settings = PlaylistSettings.Create(
            _minTempo,
            _maxTempo,
            _genreWeights,
            _discoveryLevel,
            _recentTrackThreshold);

        // Act & Assert - Test that the JSON serialization/deserialization works
        settings.GenreWeights.Should().BeEquivalentTo(_genreWeights);
        settings.GenreWeights["pop"].Should().Be(1.0m);
        settings.GenreWeights["rock"].Should().Be(0.8m);
    }
}
