// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PulsePlaylist.Domain.Common;

/// <summary>
/// Base class for auditable entities that can be soft deleted
/// </summary>
public abstract class BaseAuditableSoftDeleteEntity : BaseAuditableEntity, ISoftDelete
{
    public bool IsDeleted { get; set; }
    public DateTime? Deleted { get; set; }
    public string? DeletedBy { get; set; }
}
