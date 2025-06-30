using System;
using System.Collections.Generic;

namespace HastBuyDAL.Models;
public partial class Return
{
    public int ReturnId { get; set; }

    public int? OrderId { get; set; }

    public string? Reason { get; set; }

    public DateTime? RequestDate { get; set; }

    public string? Status { get; set; }

    public decimal? RefundAmount { get; set; }

    public virtual Order? Order { get; set; }
}
