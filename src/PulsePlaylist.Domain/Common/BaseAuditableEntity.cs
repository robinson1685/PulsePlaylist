// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PulsePlaylist.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
{
    public virtual DateTime? Created { get; set; }

    public virtual string? CreatedBy { get; set; }

    public virtual DateTime? LastModified { get; set; }

    public virtual string? LastModifiedBy { get; set; }
}

public interface IAuditableEntity
{
    DateTime? Created { get; set; }

    string? CreatedBy { get; set; }

   DateTime? LastModified { get; set; }

    string? LastModifiedBy { get; set; }
}
