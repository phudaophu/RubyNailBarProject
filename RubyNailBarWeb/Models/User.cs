using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RubyNailBarWeb.Models;

public partial class User
{
    public int UserId { get; set; }

    [Required]
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [Required]
    public string? Username { get; set; }

    [Required]
    public string? PasswordHash { get; set; }

    public string? Email { get; set; }

    public string? PhoneNo { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime? ModifiedDatetime { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Tip> Tips { get; set; } = new List<Tip>();

    public virtual ICollection<TurnLog> TurnLogs { get; set; } = new List<TurnLog>();

    public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

    public virtual ICollection<WorkDayRecord> WorkDayRecords { get; set; } = new List<WorkDayRecord>();
}
