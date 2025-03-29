using FluentAssertions;
using PulsePlaylist.Domain.Common;
using PulsePlaylist.Domain.Entities;
using PulsePlaylist.Domain.Events;
using Xunit;

namespace PulsePlaylist.Domain.Tests.Entities;

public class PlaylistItemTests
{
    private readonly string _id = "item-1";
    private readonly string _playlistId = "playlist-1";
    private readonly Track _track;
    private readonly int _position = 1;

    public PlaylistItemTests()
    {
        _track = new Track(
            id: "track-1",
            spotifyId: "spotify:track:123",
            name: "Test Track",
            artist: "Test Artist",
            durationMs: 180000);
    }

    [Fact]
    public void Constructor_WithValidParameters_CreatesPlaylistItem()
    {
        // Act
        var playlistItem = new PlaylistItem(_id, _playlistId, _track, _position);

        // Assert
        playlistItem.Id.Should().Be(_id);
        playlistItem.PlaylistId.Should().Be(_playlistId);
        playlistItem.Track.Should().Be(_track);
        playlistItem.TrackId.Should().Be(_track.Id);
        playlistItem.Position.Should().Be(_position);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void Constructor_WithInvalidPlaylistId_ThrowsArgumentException(string invalidPlaylistId)
    {
        // Act & Assert
        var act = () => new PlaylistItem(_id, invalidPlaylistId, _track, _position);
        act.Should().Throw<ArgumentException>()
            .WithMessage("Playlist ID cannot be empty*");
    }

    [Fact]
    public void Constructor_WithNullTrack_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = () => new PlaylistItem(_id, _playlistId, null!, _position);
        act.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Constructor_WithNegativePosition_ThrowsArgumentException(int invalidPosition)
    {
        // Act & Assert
        var act = () => new PlaylistItem(_id, _playlistId, _track, invalidPosition);
        act.Should().Throw<ArgumentException>()
            .WithMessage("Position cannot be negative*");
    }

    [Fact]
    public void UpdatePosition_WithValidNewPosition_UpdatesPositionAndRaisesEvent()
    {
        // Arrange
        var playlistItem = new PlaylistItem(_id, _playlistId, _track, _position);
        var newPosition = 2;

        // Act
        playlistItem.UpdatePosition(newPosition);

        // Assert
        playlistItem.Position.Should().Be(newPosition);
        var domainEvents = playlistItem.DomainEvents;
        domainEvents.Should().ContainSingle();
        var evt = domainEvents.First() as PlaylistItemReorderedEvent;
        evt.Should().NotBeNull();
        evt!.PlaylistId.Should().Be(_playlistId);
        evt.PlaylistItemId.Should().Be(_id);
        evt.NewPosition.Should().Be(newPosition);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public void UpdatePosition_WithNegativePosition_ThrowsArgumentException(int invalidPosition)
    {
        // Arrange
        var playlistItem = new PlaylistItem(_id, _playlistId, _track, _position);

        // Act & Assert
        var act = () => playlistItem.UpdatePosition(invalidPosition);
        act.Should().Throw<ArgumentException>()
            .WithMessage("Position cannot be negative*");
    }

    [Fact]
    public void UpdatePosition_WithSamePosition_DoesNotRaiseEvent()
    {
        // Arrange
        var playlistItem = new PlaylistItem(_id, _playlistId, _track, _position);

        // Act
        playlistItem.UpdatePosition(_position);

        // Assert
        playlistItem.Position.Should().Be(_position);
        playlistItem.DomainEvents.Should().BeEmpty();
    }
}
