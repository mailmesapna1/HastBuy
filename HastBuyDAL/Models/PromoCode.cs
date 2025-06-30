using System;
using System.Collections.Generic;

namespace HastBuyDAL.Models;
public partial class PromoCode
{
    public int PromoId { get; set; }

    public string? Code { get; set; }

    public int? DiscountPercent { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public int? MaxUsage { get; set; }

    public int? UsageCount { get; set; }

    public virtual ICollection<PromoUsage> PromoUsages { get; set; } = new List<PromoUsage>();
}
