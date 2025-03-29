using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PulsePlaylist.Domain.ValueObjects;

public record PlaylistSettings
{
    public required int MinTempo { get; init; }
    public required int MaxTempo { get; init; }
    private string _genreWeightsJson = "{}";

    [NotMapped]
    public Dictionary<string, decimal> GenreWeights
    {
        get => System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, decimal>>(_genreWeightsJson) ?? new();
        init => _genreWeightsJson = System.Text.Json.JsonSerializer.Serialize(value);
    }
    public required decimal DiscoveryLevel { get; init; } // 0.0 to 1.0, higher means more likely to include new tracks
    public required int RecentTrackThreshold { get; init; } // How many minutes to wait before replaying a track

    private PlaylistSettings() { } // Required for EF Core

    public static PlaylistSettings Create(
        int minTempo,
        int maxTempo,
        Dictionary<string, decimal> genreWeights,
        decimal discoveryLevel,
        int recentTrackThreshold)
    {
        // Validate parameters
        ArgumentOutOfRangeException.ThrowIfLessThan(minTempo, 40, nameof(minTempo));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(maxTempo, 200, nameof(maxTempo));
        ArgumentOutOfRangeException.ThrowIfLessThan(maxTempo, minTempo, nameof(maxTempo));
        ArgumentNullException.ThrowIfNull(genreWeights, nameof(genreWeights));
        if (genreWeights.Count == 0) throw new ArgumentException("Must have at least one genre weight", nameof(genreWeights));
        ArgumentOutOfRangeException.ThrowIfLessThan(discoveryLevel, 0, nameof(discoveryLevel));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(discoveryLevel, 1, nameof(discoveryLevel));
        ArgumentOutOfRangeException.ThrowIfLessThan(recentTrackThreshold, 0, nameof(recentTrackThreshold));

        return new PlaylistSettings
        {
            MinTempo = minTempo,
            MaxTempo = maxTempo,
            GenreWeights = new Dictionary<string, decimal>(genreWeights),
            DiscoveryLevel = discoveryLevel,
            RecentTrackThreshold = recentTrackThreshold
        };
    }

    public static PlaylistSettings CreateDefault()
    {
        return new PlaylistSettings
        {
            MinTempo = 120,
            MaxTempo = 160,
            GenreWeights = new Dictionary<string, decimal>
            {
                { "pop", 1.0m },
                { "rock", 1.0m },
                { "electronic", 1.0m }
            },
            DiscoveryLevel = 0.3m,
            RecentTrackThreshold = 120 // 2 hours in minutes
        };
    }
}
