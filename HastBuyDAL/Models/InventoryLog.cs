using System;
using System.Collections.Generic;

namespace HastBuyDAL.Models;
public partial class InventoryLog
{
    public int LogId { get; set; }

    public int? ProductId { get; set; }

    public string? ChangeType { get; set; }

    public int? QuantityChange { get; set; }

    public DateTime? ChangedAt { get; set; }

    public int? ReferenceId { get; set; }

    public virtual Product? Product { get; set; }
}
