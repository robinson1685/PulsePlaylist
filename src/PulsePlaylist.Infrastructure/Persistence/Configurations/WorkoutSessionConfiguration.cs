using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulsePlaylist.Domain.Entities;

namespace PulsePlaylist.Infrastructure.Persistence.Configurations;

public class WorkoutSessionConfiguration : IEntityTypeConfiguration<WorkoutSession>
{
    public void Configure(EntityTypeBuilder<WorkoutSession> builder)
    {
        builder.ToTable("WorkoutSessions");

        // Primary Key Configuration
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasMaxLength(36)
            .IsRequired();

        // Basic properties
        builder.Property(e => e.StartTime)
            .IsRequired();

        builder.Property(e => e.EndTime);

        builder.Property(e => e.Type)
            .IsRequired()
            .HasMaxLength(50)
            .HasConversion<string>();

        // User relationship
        builder.Property(e => e.UserId)
            .HasMaxLength(450)  // Match IdentityUser ID length
            .IsRequired();

        builder.HasIndex(e => e.UserId);
        builder.HasIndex(e => e.StartTime);

        // Owned entity types for collections
        builder.OwnsMany(e => e.IntensityData, intensityData =>
        {
            intensityData.ToTable("WorkoutIntensitySnapshots");
            intensityData.WithOwner().HasForeignKey("WorkoutSessionId");
            intensityData.Property(s => s.HeartRate).IsRequired();
            intensityData.Property(s => s.IntensityScore).IsRequired();
            intensityData.Property(s => s.Timestamp).IsRequired();

            intensityData.HasIndex(s => s.Timestamp);
        });

        builder.OwnsMany(e => e.PlayedTracks, playedTracks =>
        {
            playedTracks.ToTable("WorkoutTrackPlaybacks");
            playedTracks.WithOwner().HasForeignKey("WorkoutSessionId");

            playedTracks.Property(t => t.SpotifyTrackId)
                .HasMaxLength(50)
                .IsRequired();

            playedTracks.Property(t => t.PlayedAt)
                .IsRequired();

            playedTracks.Property(t => t.PositionInSession)
                .IsRequired();

            playedTracks.HasIndex(t => t.PlayedAt);
            playedTracks.HasIndex(t => t.SpotifyTrackId);
        });
    }
}
