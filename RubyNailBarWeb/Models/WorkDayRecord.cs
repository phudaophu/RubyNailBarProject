using System;
using System.Collections.Generic;

namespace RubyNailBarWeb.Models;

public partial class WorkDayRecord
{
    public int WorkDayRecordId { get; set; }

    public DateOnly? RecordDate { get; set; }

    public int? UserId { get; set; }

    public int? InitialOrder { get; set; }

    public bool? IsSkipped { get; set; }

    public bool? IsBusy { get; set; }

    public bool? IsIgnored { get; set; }

    public DateTime? ModifiedDatetime { get; set; }

    public virtual User? User { get; set; }
}
