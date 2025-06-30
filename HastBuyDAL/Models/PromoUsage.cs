using System;
using System.Collections.Generic;

namespace HastBuyDAL.Models;
public partial class PromoUsage
{
    public int UsageId { get; set; }

    public int? PromoId { get; set; }

    public int? UserId { get; set; }

    public DateTime? UsedOn { get; set; }

    public int? OrderId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual PromoCode? Promo { get; set; }

    public virtual User? User { get; set; }
}
