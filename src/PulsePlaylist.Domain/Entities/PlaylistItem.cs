using PulsePlaylist.Domain.Common;
using PulsePlaylist.Domain.Events;

namespace PulsePlaylist.Domain.Entities;

/// <summary>
/// Represents an item in a playlist with its position and associated track.
/// Forms part of the Playlist aggregate.
/// </summary>
public class PlaylistItem : BaseAuditableEntity
{
    public string TrackId { get; private set; }
    public Track Track { get; private set; } = null!;
    public int Position { get; private set; }
    public string PlaylistId { get; private set; } = null!;

    // Navigation property for EF Core
    public Playlist Playlist { get; private set; } = null!;

    // EF Core constructor
    private PlaylistItem() { }

    public PlaylistItem(
        string id,
        string playlistId,
        Track track,
        int position)
    {
        if (string.IsNullOrWhiteSpace(playlistId))
            throw new ArgumentException("Playlist ID cannot be empty", nameof(playlistId));
        ArgumentNullException.ThrowIfNull(track);

        if (position < 0)
            throw new ArgumentException("Position cannot be negative", nameof(position));

        Id = id;
        PlaylistId = playlistId;
        Track = track;
        TrackId = track.Id;
        Position = position;
    }

    public void UpdatePosition(int newPosition)
    {
        if (newPosition < 0)
            throw new ArgumentException("Position cannot be negative", nameof(newPosition));

        if (Position == newPosition)
            return;

        Position = newPosition;
        AddDomainEvent(new PlaylistItemReorderedEvent(PlaylistId, Id, Position));
    }
}
