using PulsePlaylist.Domain.Common;
using PulsePlaylist.Domain.ValueObjects;

namespace PulsePlaylist.Domain.Events;

public class PlaylistItemReorderedEvent : DomainEvent
{
    public PlaylistId PlaylistId { get; }
    public string PlaylistItemId { get; }
    public int NewPosition { get; }

    public PlaylistItemReorderedEvent(PlaylistId playlistId, string playlistItemId, int newPosition)
    {
        PlaylistId = playlistId;
        PlaylistItemId = playlistItemId;
        NewPosition = newPosition;
    }
}

public class PlaylistItemAddedEvent : DomainEvent
{
    public PlaylistId PlaylistId { get; }
    public string PlaylistItemId { get; }

    public PlaylistItemAddedEvent(PlaylistId playlistId, string playlistItemId)
    {
        PlaylistId = playlistId;
        PlaylistItemId = playlistItemId;
    }
}

public class PlaylistItemRemovedEvent : DomainEvent
{
    public PlaylistId PlaylistId { get; }
    public string PlaylistItemId { get; }

    public PlaylistItemRemovedEvent(PlaylistId playlistId, string playlistItemId)
    {
        PlaylistId = playlistId;
        PlaylistItemId = playlistItemId;
    }
}

public class PlaylistUpdatedEvent : DomainEvent
{
    public PlaylistId PlaylistId { get; }

    public PlaylistUpdatedEvent(PlaylistId playlistId)
    {
        PlaylistId = playlistId;
    }
}
