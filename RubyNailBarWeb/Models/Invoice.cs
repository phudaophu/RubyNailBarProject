using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RubyNailBarWeb.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }
    [Required]
    public DateOnly? InvoiceDate { get; set; }

    [Required]
    public int? StoreId { get; set; }
    public int? ManagerId { get; set; }
    [Required]
    public int? CustomerId { get; set; }

    public decimal? ServicesAmount { get; set; }

    public decimal? GSTax { get; set; }

    public decimal? PSTax { get; set; }

    public decimal? Discount { get; set; }

    public decimal? TipAmount { get; set; }

    public DateTime? CreatedDatetime { get; set; }

    public int? PaymentMethodId { get; set; }

    public bool IsDeleted { get; set; } = false;

    public bool IsImportant { get; set; } = false;

    public string? Description { get; set; }

    public string? BookingType { get; set; }

    public DateTime? ModifiedDatetime { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<CustomerPointLog> CustomerPointLogs { get; set; } = new List<CustomerPointLog>();

    public virtual ICollection<GiftCardDetailLog> GiftCardDetailLogs { get; set; } = new List<GiftCardDetailLog>();

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual User? Manager { get; set; }

    public virtual PaymentMethod? PaymentMethod { get; set; }

    public virtual Store? Store { get; set; }
}
