﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.



using PulsePlaylist.Domain.Common;
using Microsoft.AspNetCore.Identity;
using PulsePlaylist.Domain.Entities;
using PulsePlaylist.Domain.ValueObjects;

namespace PulsePlaylist.Domain.Identities;

public class ApplicationUser : IdentityUser, IAuditableEntity
{
    public string? Nickname { get; set; }
    public string? Provider { get; set; } = "Local";
    public string? TenantId { get; set; }
    public string? AvatarUrl { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string? TimeZoneId { get; set; }
    public string? LanguageCode { get; set; }
    public string? SuperiorId { get; set; } = null;
    public ApplicationUser? Superior { get; set; } = null;
    public DateTime? Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public PlaylistSettings Settings { get; set; } = PlaylistSettings.CreateDefault();
    public List<WorkoutSession> WorkoutSessions { get; private set; } = new();
}
