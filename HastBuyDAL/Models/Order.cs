using System;
using System.Collections.Generic;

namespace HastBuyDAL.Models;
public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public int? AddressId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? OrderStatus { get; set; }

    public string? PaymentStatus { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<PromoUsage> PromoUsages { get; set; } = new List<PromoUsage>();

    public virtual ICollection<Return> Returns { get; set; } = new List<Return>();

    public virtual User? User { get; set; }
}
