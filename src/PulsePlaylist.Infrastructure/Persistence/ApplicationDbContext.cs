using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PulsePlaylist.Application.Common.Interfaces;
using PulsePlaylist.Domain.Entities;
using PulsePlaylist.Domain.ValueObjects;
using PulsePlaylist.Domain.Identities;
using System.Reflection;

namespace PulsePlaylist.Infrastructure.Persistence;
/// <summary>
/// Represents the application database context.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the Tenants DbSet.
    /// </summary>
    public DbSet<Tenant> Tenants { get; set; }

    /// <summary>
    /// Gets or sets the AuditTrails DbSet.
    /// </summary> 
    public DbSet<AuditTrail> AuditTrails { get; set; }

    /// <summary>
    /// Gets or sets the Products DbSet.
    /// </summary>
    public DbSet<Product> Products { get; set; }

    /// <summary>
    /// Gets or sets the Stocks DbSet.
    /// </summary>
    public DbSet<Stock> Stocks { get; set; }

    public DbSet<WorkoutSession> WorkoutSessions => Set<WorkoutSession>();
    public DbSet<Track> Tracks => Set<Track>();
    public DbSet<AudioFeatures> AudioFeatures => Set<AudioFeatures>();
    public DbSet<Playlist> Playlists => Set<Playlist>();
    public DbSet<PlaylistItem> PlaylistItems => Set<PlaylistItem>();

    /// <summary>
    /// Configures the schema needed for the identity framework.
    /// </summary>
    /// <param name="builder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Apply configurations from the Infrastructure assembly
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Configure PlaylistSettings as owned type of ApplicationUser
        builder.Entity<ApplicationUser>()
            .OwnsOne(u => u.Settings);

    }

    /// <summary>
    /// Configures the conventions to be used for this context.
    /// </summary>
    /// <param name="configurationBuilder">The builder being used to configure conventions for this context.</param>
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<string>().HaveMaxLength(450);
    }
}
