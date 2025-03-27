using FluentAssertions;
using PulsePlaylist.Domain.ValueObjects;
using Xunit;

namespace PulsePlaylist.Domain.Tests.ValueObjects;

public class WorkoutIntensityTests
{
    [Theory]
    [InlineData(120, 60, 180, 0.5f)] // Mid-range
    [InlineData(60, 60, 180, 0.0f)]  // At resting HR
    [InlineData(180, 60, 180, 1.0f)] // At max HR
    [InlineData(90, 60, 180, 0.25f)] // Quarter intensity
    [InlineData(150, 60, 180, 0.75f)] // Three-quarter intensity
    public void FromHeartRate_WithValidInputs_ShouldCalculateCorrectIntensity(
        int currentHr, int restingHr, int maxHr, float expectedIntensity)
    {
        // Act
        var intensity = WorkoutIntensity.FromHeartRate(currentHr, restingHr, maxHr);

        // Assert
        ((float)intensity).Should().BeApproximately(expectedIntensity, 0.01f);
    }

    [Theory]
    [InlineData(0, 60, 180)]
    [InlineData(-1, 60, 180)]
    public void FromHeartRate_WithInvalidCurrentHr_ShouldThrowArgumentException(
        int invalidCurrentHr, int restingHr, int maxHr)
    {
        // Act & Assert
        var act = () => WorkoutIntensity.FromHeartRate(invalidCurrentHr, restingHr, maxHr);
        act.Should().Throw<ArgumentException>()
           .WithMessage("Current heart rate must be greater than 0*");
    }

    [Theory]
    [InlineData(120, 0, 180)]
    [InlineData(120, -1, 180)]
    public void FromHeartRate_WithInvalidRestingHr_ShouldThrowArgumentException(
        int currentHr, int invalidRestingHr, int maxHr)
    {
        // Act & Assert
        var act = () => WorkoutIntensity.FromHeartRate(currentHr, invalidRestingHr, maxHr);
        act.Should().Throw<ArgumentException>()
           .WithMessage("Resting heart rate must be greater than 0*");
    }

    [Theory]
    [InlineData(120, 60, 60)]  // Max equals resting
    [InlineData(120, 60, 59)]  // Max less than resting
    public void FromHeartRate_WithInvalidMaxHr_ShouldThrowArgumentException(
        int currentHr, int restingHr, int invalidMaxHr)
    {
        // Act & Assert
        var act = () => WorkoutIntensity.FromHeartRate(currentHr, restingHr, invalidMaxHr);
        act.Should().Throw<ArgumentException>()
           .WithMessage("Max heart rate must be greater than resting heart rate*");
    }

    [Theory]
    [InlineData(120, 60, 180, 0.5f, WorkoutIntensity.Zone.Medium)]  // 0.5 -> Medium
    [InlineData(90, 60, 180, 0.25f, WorkoutIntensity.Zone.Low)]     // 0.25 -> Low
    [InlineData(165, 60, 180, 0.875f, WorkoutIntensity.Zone.High)]  // 0.875 -> High
    [InlineData(108, 60, 180, 0.4f, WorkoutIntensity.Zone.Low)]     // 0.4 -> Low (boundary)
    [InlineData(144, 60, 180, 0.7f, WorkoutIntensity.Zone.High)]    // 0.7 -> High (boundary)
    public void GetZone_ShouldReturnCorrectZone(
        int currentHr, int restingHr, int maxHr, float expectedIntensity, WorkoutIntensity.Zone expectedZone)
    {
        // Arrange
        var intensity = WorkoutIntensity.FromHeartRate(currentHr, restingHr, maxHr);

        // Assert
        ((float)intensity).Should().BeApproximately(expectedIntensity, 0.01f);
        intensity.GetZone().Should().Be(expectedZone);
    }

    [Fact]
    public void FromHeartRate_WithCurrentHrBelowResting_ShouldReturnZeroIntensity()
    {
        // Arrange
        var currentHr = 55;
        var restingHr = 60;
        var maxHr = 180;

        // Act
        var intensity = WorkoutIntensity.FromHeartRate(currentHr, restingHr, maxHr);

        // Assert
        ((float)intensity).Should().Be(0);
        intensity.GetZone().Should().Be(WorkoutIntensity.Zone.Low);
    }

    [Fact]
    public void FromHeartRate_WithCurrentHrAboveMax_ShouldReturnFullIntensity()
    {
        // Arrange
        var currentHr = 190;
        var restingHr = 60;
        var maxHr = 180;

        // Act
        var intensity = WorkoutIntensity.FromHeartRate(currentHr, restingHr, maxHr);

        // Assert
        ((float)intensity).Should().Be(1);
        intensity.GetZone().Should().Be(WorkoutIntensity.Zone.High);
    }
}
