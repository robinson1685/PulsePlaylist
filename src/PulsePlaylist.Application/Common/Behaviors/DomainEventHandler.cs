// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Mediator;
using Microsoft.Extensions.Logging;
using PulsePlaylist.Domain;

namespace PulsePlaylist.Application.Common.Behaviors;

/// <summary>
/// Base handler for all domain events with built-in logging
/// </summary>
/// <typeparam name="TEvent">The type of domain event</typeparam>
public abstract class DomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : DomainEvent
{
    private readonly ILogger _logger;

    protected DomainEventHandler(ILogger logger)
    {
        _logger = logger;
    }

    public async ValueTask Handle(TEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling domain event {EventType} with ID {EventId}",
            typeof(TEvent).Name, notification.EventId);

        await HandleEvent(notification, cancellationToken);

        _logger.LogInformation("Handled domain event {EventType} with ID {EventId}",
            typeof(TEvent).Name, notification.EventId);
    }

    /// <summary>
    /// Handle the specific domain event
    /// </summary>
    /// <param name="domainEvent">The domain event to handle</param>
    /// <param name="cancellationToken">Cancellation token</param>
    protected abstract Task HandleEvent(TEvent domainEvent, CancellationToken cancellationToken);
}