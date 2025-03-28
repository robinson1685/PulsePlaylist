// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PulsePlaylist.Domain.Common;

namespace PulsePlaylist.Domain.Entities;

/// <summary>
/// Stock Entity
/// </summary>
public class Stock : BaseEntity, IAuditTrail
{
    /// <summary>
    /// Gets or sets the product ID.
    /// </summary>
    public string? ProductId { get; set; }

    /// <summary>
    /// Gets or sets the product associated with the stock.
    /// </summary>
    public Product? Product { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the stock.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the location of the stock.
    /// </summary>
    public string Location { get; set; } = string.Empty;

    public DateTimeOffset? Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
