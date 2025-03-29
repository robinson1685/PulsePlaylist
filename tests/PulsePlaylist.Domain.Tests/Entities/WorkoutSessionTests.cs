using FluentAssertions;
using PulsePlaylist.Domain.Entities;
using Xunit;

namespace PulsePlaylist.Domain.Tests.Entities;

public class WorkoutSessionTests
{
    private readonly string _userId = "user-1";
    private readonly WorkoutType _type = WorkoutType.Run;

    [Fact]
    public void Create_WithValidParameters_CreatesWorkoutSession()
    {
        // Act
        var session = WorkoutSession.Create(_userId, _type);

        // Assert
        session.Id.Should().NotBeNullOrEmpty();
        session.UserId.Should().Be(_userId);
        session.Type.Should().Be(_type);
        session.StartTime.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        session.EndTime.Should().BeNull();
        session.IntensityData.Should().BeEmpty();
        session.PlayedTracks.Should().BeEmpty();
    }

    [Fact]
    public void End_WhenSessionActive_SetsEndTime()
    {
        // Arrange
        var session = WorkoutSession.Create(_userId, _type);

        // Act
        session.End();

        // Assert
        session.EndTime.Should().NotBeNull();
        session.EndTime.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void End_WhenSessionAlreadyEnded_ThrowsException()
    {
        // Arrange
        var session = WorkoutSession.Create(_userId, _type);
        session.End();

        // Act & Assert
        var act = () => session.End();
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Workout session already ended");
    }

    [Fact]
    public void AddIntensitySnapshot_WhenSessionActive_AddsSnapshot()
    {
        // Arrange
        var session = WorkoutSession.Create(_userId, _type);
        var heartRate = 150.0;
        var intensity = 0.8;

        // Act
        session.AddIntensitySnapshot(heartRate, intensity);

        // Assert
        session.IntensityData.Should().ContainSingle();
        var snapshot = session.IntensityData[0];
        snapshot.HeartRate.Should().Be(heartRate);
        snapshot.IntensityScore.Should().Be(intensity);
        snapshot.Timestamp.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void AddIntensitySnapshot_WhenSessionEnded_ThrowsException()
    {
        // Arrange
        var session = WorkoutSession.Create(_userId, _type);
        session.End();

        // Act & Assert
        var act = () => session.AddIntensitySnapshot(150.0, 0.8);
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Cannot add data to ended session");
    }

    [Fact]
    public void AddTrackPlayback_WhenSessionActive_AddsTrack()
    {
        // Arrange
        var session = WorkoutSession.Create(_userId, _type);
        var trackId = "spotify:track:123";

        // Act
        session.AddTrackPlayback(trackId);

        // Assert
        session.PlayedTracks.Should().ContainSingle();
        var playback = session.PlayedTracks[0];
        playback.SpotifyTrackId.Should().Be(trackId);
        playback.PlayedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        playback.PositionInSession.Should().Be(0);
    }

    [Fact]
    public void AddTrackPlayback_WhenSessionEnded_ThrowsException()
    {
        // Arrange
        var session = WorkoutSession.Create(_userId, _type);
        session.End();

        // Act & Assert
        var act = () => session.AddTrackPlayback("spotify:track:123");
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Cannot add tracks to ended session");
    }

    [Fact]
    public void AddTrackPlayback_MultipleTracks_SetsCorrectPositions()
    {
        // Arrange
        var session = WorkoutSession.Create(_userId, _type);

        // Act
        session.AddTrackPlayback("spotify:track:1");
        session.AddTrackPlayback("spotify:track:2");
        session.AddTrackPlayback("spotify:track:3");

        // Assert
        session.PlayedTracks.Should().HaveCount(3);
        session.PlayedTracks[0].PositionInSession.Should().Be(0);
        session.PlayedTracks[1].PositionInSession.Should().Be(1);
        session.PlayedTracks[2].PositionInSession.Should().Be(2);
    }

    [Fact]
    public void CalculateAverageIntensity_WithNoData_ReturnsNull()
    {
        // Arrange
        var session = WorkoutSession.Create(_userId, _type);

        // Act
        var average = session.CalculateAverageIntensity();

        // Assert
        average.Should().BeNull();
    }

    [Fact]
    public void CalculateAverageIntensity_WithData_ReturnsCorrectAverage()
    {
        // Arrange
        var session = WorkoutSession.Create(_userId, _type);
        session.AddIntensitySnapshot(150.0, 0.5);
        session.AddIntensitySnapshot(160.0, 0.7);
        session.AddIntensitySnapshot(170.0, 0.9);

        // Act
        var average = session.CalculateAverageIntensity();

        // Assert
        average.Should().BeApproximately(0.7, 0.000001);
    }
}
