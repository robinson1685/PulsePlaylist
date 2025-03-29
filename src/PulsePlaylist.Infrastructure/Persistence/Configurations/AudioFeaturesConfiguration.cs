using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulsePlaylist.Domain.Entities;

namespace PulsePlaylist.Infrastructure.Persistence.Configurations;

public class AudioFeaturesConfiguration : IEntityTypeConfiguration<AudioFeatures>
{
    public void Configure(EntityTypeBuilder<AudioFeatures> builder)
    {
        builder.ToTable("AudioFeatures");

        // Primary Key Configuration
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasMaxLength(36)
            .IsRequired();

        // Track relationship
        builder.Property(x => x.TrackId)
            .HasMaxLength(36)
            .IsRequired();

        builder.HasOne(x => x.Track)
            .WithOne(x => x.Features)
            .HasForeignKey<AudioFeatures>(x => x.TrackId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Spotify integration
        builder.Property(x => x.SpotifyId)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(x => x.SpotifyId)
            .IsUnique();

        // Audio characteristics
        builder.Property(x => x.Bpm)
            .IsRequired();

        builder.Property(x => x.Energy)
            .IsRequired();

        builder.Property(x => x.Valence)
            .IsRequired();

        // Create indexes for common queries
        builder.HasIndex(x => x.Bpm);
        builder.HasIndex(x => new { x.Energy, x.Valence });
    }
}
