using System;
using System.Collections.Generic;

namespace RubyNailBarWeb.Models;

public partial class GiftCardDetailLog
{
    public int GiftCardLogId { get; set; }

    public int? GiftCardId { get; set; }

    public string? GiftCardUser { get; set; }

    public int? InvoiceId { get; set; }

    public string? Decription { get; set; }

    public DateTime? CreatedDatetime { get; set; }

    public virtual GiftCard? GiftCard { get; set; }

    public virtual Invoice? Invoice { get; set; }
}
