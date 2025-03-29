using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulsePlaylist.Domain.Entities;

namespace PulsePlaylist.Infrastructure.Persistence.Configurations;

public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
{
    public void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.ToTable("Playlists");

        // Primary Key Configuration
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(36)
            .IsRequired();

        // Column Configurations
        builder.Property(p => p.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        builder.Property(p => p.OwnerId)
            .HasMaxLength(450)  // Match IdentityUser ID length
            .IsRequired();

        builder.Property(p => p.SpotifyId)
            .HasMaxLength(50);

        // Index for better lookup performance
        builder.HasIndex(p => p.OwnerId);
        builder.HasIndex(p => p.SpotifyId);
    }
}
