// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PulsePlaylist.Domain.Common;

/// <summary>
/// Marker interface to identify aggregate roots in the domain model.
/// An aggregate root is an entity that acts as the entry point to an aggregate
/// and is the only entity accessible from outside the aggregate.
/// </summary>
public interface IAggregateRoot
{
}