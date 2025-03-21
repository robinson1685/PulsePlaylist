﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.



using PulsePlaylist.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace PulsePlaylist.Domain.Identities;

public class ApplicationUser : IdentityUser, IAuditableEntity
{
    public string? Nickname { get; set; }
    public string? Provider { get; set; } = "Local";
    public string? TenantId { get; set; }
    public string? AvatarUrl { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    //public string? SpotifyId { get; set; }
    //public string? GoogleId { get; set; }
    public string? TimeZoneId { get; set; }
    public string? LanguageCode { get; set; }
    public string? SuperiorId { get; set; } = null;
    public ApplicationUser? Superior { get; set; } = null;
    public DateTime? Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }

    //public void SetSpotifyConnection(string spotifyId)
    //{
    //    if (string.IsNullOrWhiteSpace(spotifyId))
    //        throw new ArgumentException("Spotify ID cannot be empty", nameof(spotifyId));

    //    SpotifyId = spotifyId;
    //}

    //public void SetGoogleConnection(string googleId)
    //{
    //    if (string.IsNullOrWhiteSpace(googleId))
    //        throw new ArgumentException("Google ID cannot be empty", nameof(googleId));

    //    GoogleId = googleId;
    //}
}


