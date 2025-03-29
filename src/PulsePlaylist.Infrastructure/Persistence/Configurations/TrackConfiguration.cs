using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulsePlaylist.Domain.Entities;

namespace PulsePlaylist.Infrastructure.Persistence.Configurations;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.ToTable("Tracks");

        // Primary Key Configuration
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasMaxLength(36)
            .IsRequired();

        // Spotify-related properties
        builder.Property(x => x.SpotifyId)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(x => x.SpotifyId)
            .IsUnique();

        // Track metadata
        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Artist)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.DurationMs)
            .IsRequired();

        builder.Property(x => x.SpotifyUri)
            .HasConversion(
                v => v != null ? v.ToString() : null,
                v => v != null ? new Uri(v) : null);

        // Audio Features relationship - Ensure required with cascade delete
        builder.HasOne(x => x.Features)
            .WithOne(x => x.Track)
            .HasForeignKey<AudioFeatures>(x => x.TrackId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Create indexes for commonly queried fields
        builder.HasIndex(x => x.Artist);
        builder.HasIndex(x => x.Name);
    }
}
