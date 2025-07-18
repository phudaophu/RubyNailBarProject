using System;
using System.Collections.Generic;

namespace RubyNailBarWeb.Models;

public partial class UserGroup
{
    public int UserGroupId { get; set; }

    public string? GroupName { get; set; }

    public string? RoleName { get; set; }

    public int? StoreId { get; set; }

    public int? UserId { get; set; }

    public virtual Store? Store { get; set; }

    public virtual User? User { get; set; }
}
