using System;
using System.Collections.Generic;

namespace RubyNailBarWeb.Models;

public partial class BirthdayNotificationLog
{
    public int BirthdayNotificationLogId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? CreatedDatetime { get; set; }

    public string? CouponCode { get; set; }

    public DateTime? ModifiedDatetime { get; set; }

    public virtual Customer? Customer { get; set; }
}
