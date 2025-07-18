using System;
using System.Collections.Generic;

namespace RubyNailBarWeb.Models;

public partial class Store
{
    public int StoreId { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
}
