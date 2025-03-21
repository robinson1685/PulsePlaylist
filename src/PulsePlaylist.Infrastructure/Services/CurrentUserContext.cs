using System.Security.Claims;
using PulsePlaylist.Application.Common.Services;

namespace PulsePlaylist.Infrastructure.Services;

/// <summary>
/// Represents the current user context, holding session information.
/// </summary>
public class CurrentUserContext : ICurrentUserContext
{
    private static AsyncLocal<ClaimsPrincipal?> _currentUser = new AsyncLocal<ClaimsPrincipal?>();

    public ClaimsPrincipal? GetCurrentUser() => _currentUser.Value;

    public void Set(ClaimsPrincipal? user) => _currentUser.Value = user;
    public void Clear() => _currentUser.Value = null;
}
