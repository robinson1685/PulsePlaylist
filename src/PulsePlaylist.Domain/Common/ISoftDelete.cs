// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PulsePlaylist.Domain.Common;

/// <summary>
/// Interface for entities that support soft deletion
/// </summary>
public interface ISoftDelete
{
    bool IsDeleted { get; set; }
    DateTime? Deleted { get; set; }
    string? DeletedBy { get; set; }
}
