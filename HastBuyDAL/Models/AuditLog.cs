using System;
using System.Collections.Generic;

namespace HastBuyDAL.Models;
public partial class AuditLog
{
    public int LogId { get; set; }

    public int? UserId { get; set; }

    public string? Action { get; set; }

    public DateTime? ActionTime { get; set; }

    public string? Ipaddress { get; set; }

    public string? Module { get; set; }
}
