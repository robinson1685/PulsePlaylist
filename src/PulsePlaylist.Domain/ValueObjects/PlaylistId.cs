namespace PulsePlaylist.Domain.ValueObjects;

public record PlaylistId
{
    public Guid Value { get; }

    private PlaylistId(Guid value)
    {
        Value = value;
    }

    public static PlaylistId Create(Guid value)
    {
        return new PlaylistId(value);
    }

    public static PlaylistId CreateUnique()
    {
        return new PlaylistId(Guid.NewGuid());
    }

    public static implicit operator Guid(PlaylistId id) => id.Value;

    public override string ToString() => Value.ToString();
}
