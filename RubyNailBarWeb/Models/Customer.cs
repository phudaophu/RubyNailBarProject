using System;
using System.Collections.Generic;

namespace RubyNailBarWeb.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? PhoneNo { get; set; }

    public DateOnly? Birthday { get; set; }

    public int? Points { get; set; }

    public int? TransactionCount { get; set; }

    public string? CustomerLevel { get; set; }

    public DateTime? CreatedDatetime { get; set; }

    public DateTime? LastVisitDatetime { get; set; }

    public string? ImageUrl { get; set; }

    public string? Address1 { get; set; }

    public DateTime? ModifiedDatetime { get; set; }

    public Decimal? LifetimeSpend { get; set; }

    public string? CivicAddress { get; set; } 

    public string? CityName { get; set; }

    public string? ProvinceName { get; set; }

    public string? PostalCode { get; set; }

    public string? CountryName { get; set; }

    public string? Description { get; set; }    

    public virtual ICollection<BirthdayNotificationLog> BirthdayNotificationLogs { get; set; } = new List<BirthdayNotificationLog>();

    public virtual ICollection<CustomerPointLog> CustomerPointLogs { get; set; } = new List<CustomerPointLog>();

    public virtual ICollection<GiftCard> GiftCards { get; set; } = new List<GiftCard>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
