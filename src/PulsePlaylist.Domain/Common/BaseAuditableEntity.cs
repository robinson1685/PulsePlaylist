// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PulsePlaylist.Domain.Common;

/// <summary>
/// Base class for all auditable entities
/// </summary>
public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
{
    public DateTime? Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
