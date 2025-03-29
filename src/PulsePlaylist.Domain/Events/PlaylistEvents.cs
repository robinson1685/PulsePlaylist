using PulsePlaylist.Domain.Common;
using PulsePlaylist.Domain.ValueObjects;

namespace PulsePlaylist.Domain.Events;

public class PlaylistItemReorderedEvent : DomainEvent
{
    public string PlaylistId { get; }
    public string PlaylistItemId { get; }
    public int NewPosition { get; }

    public PlaylistItemReorderedEvent(string playlistId, string playlistItemId, int newPosition)
    {
        PlaylistId = playlistId;
        PlaylistItemId = playlistItemId;
        NewPosition = newPosition;
    }
}

public class PlaylistItemAddedEvent : DomainEvent
{
    public string PlaylistId { get; }
    public string PlaylistItemId { get; }

    public PlaylistItemAddedEvent(string playlistId, string playlistItemId)
    {
        PlaylistId = playlistId;
        PlaylistItemId = playlistItemId;
    }
}

public class PlaylistItemRemovedEvent : DomainEvent
{
    public string PlaylistId { get; }
    public string PlaylistItemId { get; }

    public PlaylistItemRemovedEvent(string playlistId, string playlistItemId)
    {
        PlaylistId = playlistId;
        PlaylistItemId = playlistItemId;
    }
}

public class PlaylistUpdatedEvent : DomainEvent
{
    public string PlaylistId { get; }

    public PlaylistUpdatedEvent(string playlistId)
    {
        PlaylistId = playlistId;
    }
}
