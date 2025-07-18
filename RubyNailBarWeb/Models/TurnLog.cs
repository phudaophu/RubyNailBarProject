using System;
using System.Collections.Generic;

namespace RubyNailBarWeb.Models;

public partial class TurnLog
{
    public int TurnLogId { get; set; }

    public int? UserId { get; set; }

    public string? Action { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedDatetime { get; set; }

    public virtual User? User { get; set; }
}
