﻿using System;
using System.Collections.Generic;

namespace HastBuyDAL.Models;
public partial class Payment
{
    public int PaymentId { get; set; }

    public int? OrderId { get; set; }

    public string? PaymentMethod { get; set; }

    public DateTime? PaymentDate { get; set; }

    public decimal? Amount { get; set; }

    public string? TransactionId { get; set; }

    public virtual Order? Order { get; set; }
}
