using System;
using System.Collections.Generic;

namespace RubyNailBarWeb.Models;

public partial class Tip
{
    public int TipId { get; set; }

    public DateOnly? TipDate { get; set; }

    public int? UserId { get; set; }

    public decimal? TotalAmount { get; set; }

    public bool? IsPaid { get; set; }

    public string? Description { get; set; }

    public DateTime? ModifiedDatetime { get; set; }

    public DateTime? IsPaidDatetime { get; set; }

    public decimal? Balance { get; set; }

    public virtual ICollection<TipLog> TipLogs { get; set; } = new List<TipLog>();

    public virtual User? User { get; set; }
}
