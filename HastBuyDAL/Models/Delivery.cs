using System;
using System.Collections.Generic;

namespace HastBuyDAL.Models;
public partial class Delivery
{
    public int DeliveryId { get; set; }

    public int? OrderId { get; set; }

    public int? DeliveryAgentId { get; set; }

    public DateTime? AssignedAt { get; set; }

    public DateTime? DeliveredAt { get; set; }

    public string? DeliveryStatus { get; set; }

    public virtual User? DeliveryAgent { get; set; }

    public virtual Order? Order { get; set; }
}
