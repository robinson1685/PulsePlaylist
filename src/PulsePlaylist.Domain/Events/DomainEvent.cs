// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Mediator;

namespace PulsePlaylist.Domain;

/// <summary>
/// Base class for all domain events
/// </summary>
public abstract class DomainEvent : INotification
{
    protected DomainEvent()
    {
        EventId = Guid.NewGuid();
        DateOccurred = DateTimeOffset.UtcNow;
    }

    public Guid EventId { get; }
    public bool IsPublished { get; set; }
    public DateTimeOffset DateOccurred { get; }
}

