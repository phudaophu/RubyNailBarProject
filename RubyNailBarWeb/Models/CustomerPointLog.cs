using System;
using System.Collections.Generic;

namespace RubyNailBarWeb.Models;

public partial class CustomerPointLog
{
    public int CustomerPointLogId { get; set; }

    public int? CustomerId { get; set; }

    public int? InvoiceId { get; set; }

    public int? Points { get; set; }

    public DateTime? CreatedDatetime { get; set; }

    public DateTime? ModifiedDatetime { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Invoice? Invoice { get; set; }
}
