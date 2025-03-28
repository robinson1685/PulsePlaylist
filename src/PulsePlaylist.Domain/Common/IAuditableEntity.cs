// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PulsePlaylist.Domain.Common;

/// <summary>
/// Interface for entities that track creation and modification audit information
/// </summary>
public interface IAuditableEntity
{
    DateTime? Created { get; set; }
    string? CreatedBy { get; set; }
    DateTime? LastModified { get; set; }
    string? LastModifiedBy { get; set; }
}