using System;
using System.Collections.Generic;

namespace RubyNailBarWeb.Models;

public partial class TipLog
{
    public int TipLogId { get; set; }

    public int? TipId { get; set; }

    public int? InvoiceDetailId { get; set; }

    public DateTime? CreatedDatetime { get; set; }

    public string? Action { get; set; }

    public string? DataName { get; set; }

    public string? DataValue { get; set; }

    public string? CreatedBy { get; set; }

    public virtual InvoiceDetail? InvoiceDetail { get; set; }

    public virtual Tip? Tip { get; set; }
}
