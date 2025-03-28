namespace PulsePlaylist.Domain.Enums;

public enum PlaylistType
{
    /// <summary>
    /// A playlist created manually by the user within the application
    /// </summary>
    UserCreated = 0,

    /// <summary>
    /// A playlist imported from the user's Spotify account
    /// </summary>
    ImportedSpotify = 1,

    /// <summary>
    /// A dynamically generated playlist for workout sessions
    /// </summary>
    AdaptiveWorkout = 2,

    /// <summary>
    /// A system-generated playlist based on user preferences
    /// </summary>
    SystemGenerated = 3
}
