using System;
using System.Collections.Generic;
using PulsePlaylist.Domain.Common;

namespace PulsePlaylist.Domain.Entities;

public class WorkoutSession : BaseAuditableEntity
{
    public required DateTimeOffset StartTime { get; init; }
    public DateTimeOffset? EndTime { get; private set; }
    public required WorkoutType Type { get; init; }
    public List<WorkoutIntensitySnapshot> IntensityData { get; private set; } = new();
    public List<TrackPlayback> PlayedTracks { get; private set; } = new();

    // User relationship - Domain should not reference Infrastructure
    public required string UserId { get; init; }

    private WorkoutSession() { } // Required for EF Core

    public static WorkoutSession Create(
        string userId,
        WorkoutType type)
    {
        return new WorkoutSession
        {
            Id = Guid.CreateVersion7().ToString(),
            UserId = userId,
            Type = type,
            StartTime = DateTimeOffset.UtcNow
        };
    }

    public void End()
    {
        if (EndTime.HasValue)
            throw new InvalidOperationException("Workout session already ended");

        EndTime = DateTimeOffset.UtcNow;
    }

    public void AddIntensitySnapshot(double heartRate, double intensityScore)
    {
        if (EndTime.HasValue)
            throw new InvalidOperationException("Cannot add data to ended session");

        IntensityData.Add(new WorkoutIntensitySnapshot
        {
            Timestamp = DateTimeOffset.UtcNow,
            HeartRate = heartRate,
            IntensityScore = intensityScore
        });
    }

    public void AddTrackPlayback(string spotifyTrackId)
    {
        if (EndTime.HasValue)
            throw new InvalidOperationException("Cannot add tracks to ended session");

        PlayedTracks.Add(new TrackPlayback
        {
            SpotifyTrackId = spotifyTrackId,
            PlayedAt = DateTimeOffset.UtcNow,
            PositionInSession = PlayedTracks.Count
        });
    }

    public double? CalculateAverageIntensity()
    {
        if (IntensityData.Count == 0)
            return null;

        var sum = 0.0;
        foreach (var snapshot in IntensityData)
            sum += snapshot.IntensityScore;

        return sum / IntensityData.Count;
    }
}

public enum WorkoutType
{
    Run,
    Cycle,
    GeneralCardio
}

public class WorkoutIntensitySnapshot
{
    public required DateTimeOffset Timestamp { get; init; }
    public required double HeartRate { get; init; }
    public required double IntensityScore { get; init; } // 0.0 to 1.0
}

public class TrackPlayback
{
    public required DateTimeOffset PlayedAt { get; init; }
    public required string SpotifyTrackId { get; init; }
    public required int PositionInSession { get; init; }
}
