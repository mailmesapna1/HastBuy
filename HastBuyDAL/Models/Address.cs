using System;
using System.Collections.Generic;

namespace HastBuyDAL.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public int? UserId { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public bool? IsDefault { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User? User { get; set; }
}
