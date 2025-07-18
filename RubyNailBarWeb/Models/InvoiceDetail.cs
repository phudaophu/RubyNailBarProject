using System;
using System.Collections.Generic;

namespace RubyNailBarWeb.Models;

public partial class InvoiceDetail
{
    public int InvoiceDetailId { get; set; }

    public int? InvoiceId { get; set; }

    public int? ServiceId { get; set; }

    public int? UserId { get; set; }

    public decimal? ServiceFee { get; set; }

    public decimal? Tip { get; set; }

    public DateTime? CreatedDatetime { get; set; }

    public virtual Invoice? Invoice { get; set; }

    public virtual Service? Service { get; set; }

    public virtual ICollection<TipLog> TipLogs { get; set; } = new List<TipLog>();

    public virtual User? User { get; set; }
}
