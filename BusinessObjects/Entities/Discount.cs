using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class Discount
{
    public int DiscountId { get; set; }

    public string? DiscountCode { get; set; }

    public string? DiscountName { get; set; }

    public decimal? DiscountValue { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ExpiredDate { get; set; }

    public Guid? CreatedById { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
