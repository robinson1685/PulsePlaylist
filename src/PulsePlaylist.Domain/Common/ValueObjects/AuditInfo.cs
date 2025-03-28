// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PulsePlaylist.Domain.Common.ValueObjects;

/// <summary>
/// Value object to encapsulate audit information
/// </summary>
public record AuditInfo
{
    public DateTime? Created { get; init; }
    public string? CreatedBy { get; init; }
    public DateTime? LastModified { get; init; }
    public string? LastModifiedBy { get; init; }
}