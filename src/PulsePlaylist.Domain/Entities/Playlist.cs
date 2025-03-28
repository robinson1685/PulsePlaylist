using PulsePlaylist.Domain.Common;
using PulsePlaylist.Domain.Enums;
using PulsePlaylist.Domain.Events;
using PulsePlaylist.Domain.ValueObjects;

namespace PulsePlaylist.Domain.Entities;

public class Playlist : BaseAuditableEntity
{
    private readonly List<PlaylistItem> _items = new();

    public PlaylistId PlaylistId { get; private set; }
    public string Name { get; private set; }
    public PlaylistType Type { get; private set; }
    public Guid OwnerId { get; private set; }
    public string? Description { get; private set; }
    public bool IsPublic { get; private set; }
    public string? SpotifyId { get; private set; }

    public IReadOnlyCollection<PlaylistItem> Items => _items.AsReadOnly();

    // EF Core constructor
    private Playlist() { }

    public Playlist(
        PlaylistId playlistId,
        string name,
        PlaylistType type,
        Guid ownerId,
        string? description = null,
        bool isPublic = false,
        string? spotifyId = null)
    {
        PlaylistId = playlistId;
        Name = name;
        Type = type;
        OwnerId = ownerId;
        Description = description;
        IsPublic = isPublic;
        SpotifyId = spotifyId;
    }

    public PlaylistItem AddTrack(Track track, int? position = null)
    {
        ArgumentNullException.ThrowIfNull(track);

        var newPosition = position ?? _items.Count + 1;

        if (position.HasValue)
        {
            // Shift existing items to accommodate the new position
            foreach (var item in _items.Where(i => i.Position >= newPosition))
            {
                item.UpdatePosition(item.Position + 1);
            }
        }

        var playlistItem = new PlaylistItem(
            Guid.NewGuid().ToString(),
            PlaylistId,
            track,
            newPosition
        );

        _items.Add(playlistItem);
        AddDomainEvent(new PlaylistItemAddedEvent(PlaylistId, playlistItem.Id));

        return playlistItem;
    }

    public void RemoveTrack(string playlistItemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == playlistItemId);
        if (item == null) return;

        var removedPosition = item.Position;
        _items.Remove(item);

        // Reorder remaining items
        foreach (var remainingItem in _items.Where(i => i.Position > removedPosition))
        {
            remainingItem.UpdatePosition(remainingItem.Position - 1);
        }

        AddDomainEvent(new PlaylistItemRemovedEvent(PlaylistId, playlistItemId));
    }

    public void ReorderTrack(string playlistItemId, int newPosition)
    {
        if (newPosition < 1 || newPosition > _items.Count)
            throw new ArgumentException("Invalid position", nameof(newPosition));

        var item = _items.FirstOrDefault(i => i.Id == playlistItemId);
        if (item == null)
            throw new InvalidOperationException($"PlaylistItem {playlistItemId} not found");

        if (item.Position == newPosition) return;

        var oldPosition = item.Position;

        // Update positions of items between old and new positions
        if (newPosition > oldPosition)
        {
            foreach (var itemToShift in _items.Where(i => i.Position > oldPosition && i.Position <= newPosition))
            {
                itemToShift.UpdatePosition(itemToShift.Position - 1);
            }
        }
        else
        {
            foreach (var itemToShift in _items.Where(i => i.Position >= newPosition && i.Position < oldPosition))
            {
                itemToShift.UpdatePosition(itemToShift.Position + 1);
            }
        }

        item.UpdatePosition(newPosition);
    }

    public void UpdateDetails(string name, string? description = null, bool? isPublic = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));

        Name = name;
        Description = description ?? Description;
        IsPublic = isPublic ?? IsPublic;

        AddDomainEvent(new PlaylistUpdatedEvent(PlaylistId));
    }

    public PlaylistItem? GetTrackAtPosition(int position)
    {
        return _items.FirstOrDefault(i => i.Position == position);
    }

    public PlaylistItem? GetNextTrackInSequence(string currentPlaylistItemId)
    {
        var currentItem = _items.FirstOrDefault(i => i.Id == currentPlaylistItemId);
        if (currentItem == null) return null;

        return _items
            .OrderBy(i => i.Position)
            .FirstOrDefault(i => i.Position > currentItem.Position);
    }
}
