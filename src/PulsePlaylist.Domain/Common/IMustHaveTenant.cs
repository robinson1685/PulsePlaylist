namespace PulsePlaylist.Domain.Common;

/// <summary>
/// Entities that must belong to a specific tenant
/// </summary>
public interface IMustHaveTenant
{
    string TenantId { get; set; }
}

/// <summary>
/// Entities that can optionally belong to a tenant
/// </summary>
public interface IMayHaveTenant
{
    string? TenantId { get; set; }
}
