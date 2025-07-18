using System;
using System.Collections.Generic;

namespace RubyNailBarWeb.Models;

public partial class GiftCard
{
    public int GiftCardId { get; set; }

    public DateOnly? GiftCardDate { get; set; }

    public string? SerialNumber { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? IssuedDatetime { get; set; }

    public decimal? FirstValue { get; set; }

    public decimal? Balance { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedDatetime { get; set; }

    public DateTime? ModifiedDatetime { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<GiftCardDetailLog> GiftCardDetailLogs { get; set; } = new List<GiftCardDetailLog>();
}
