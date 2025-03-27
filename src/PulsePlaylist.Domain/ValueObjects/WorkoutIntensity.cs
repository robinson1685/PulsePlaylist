namespace PulsePlaylist.Domain.ValueObjects;

public readonly record struct WorkoutIntensity
{
    private readonly float _value;

    private WorkoutIntensity(float value)
    {
        if (value < 0 || value > 1)
            throw new ArgumentException("Intensity value must be between 0 and 1", nameof(value));

        _value = value;
    }

    public static WorkoutIntensity FromHeartRate(int currentHr, int restingHr, int maxHr)
    {
        if (currentHr <= 0)
            throw new ArgumentException("Current heart rate must be greater than 0", nameof(currentHr));

        if (restingHr <= 0)
            throw new ArgumentException("Resting heart rate must be greater than 0", nameof(restingHr));

        if (maxHr <= restingHr)
            throw new ArgumentException("Max heart rate must be greater than resting heart rate", nameof(maxHr));

        if (currentHr < restingHr)
            return new WorkoutIntensity(0);

        if (currentHr > maxHr)
            return new WorkoutIntensity(1);

        var intensity = (float)(currentHr - restingHr) / (maxHr - restingHr);
        return new WorkoutIntensity(intensity);
    }

    public Zone GetZone() => _value switch
    {
        <= 0.4f => Zone.Low,
        < 0.7f => Zone.Medium,
        _ => Zone.High
    };

    public float Value => _value;

    public enum Zone
    {
        Low,
        Medium,
        High
    }

    public static implicit operator float(WorkoutIntensity intensity) => intensity._value;
}
