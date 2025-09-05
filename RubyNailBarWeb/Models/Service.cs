using System;
using System.Collections.Generic;

namespace RubyNailBarWeb.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string? Name { get; set; }

    public string? VnName { get; set; }

    public decimal? Fee { get; set; }

    public int? DurationMinutes { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public int? AdditionalParameterA { get; set; } = 0;

    public string? FormularA { get; set; } = string.Empty;

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
}
