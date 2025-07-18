using System;
using System.Collections.Generic;

namespace RubyNailBarWeb.Models;

public partial class PaymentMethod
{
    public int PaymentMethodId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
