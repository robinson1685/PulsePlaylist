using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulsePlaylist.Domain.Entities;

namespace PulsePlaylist.Infrastructure.Persistence.Configurations;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.ToTable("Tracks");

        builder.Property(t => t.SpotifyId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(t => t.SpotifyId)
            .IsUnique();

        // SQLite-compatible configuration for decimals
        builder.Property(t => t.Tempo)
            .HasColumnType("REAL"); // REAL for SQLite floating-point numbers

        builder.Property(t => t.Energy)
            .HasColumnType("REAL");

        builder.Property(t => t.Danceability)
            .HasColumnType("REAL");

        builder.Property(t => t.Valence)
            .HasColumnType("REAL");
    }
}
