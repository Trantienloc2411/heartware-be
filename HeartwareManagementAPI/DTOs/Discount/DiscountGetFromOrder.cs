using System;

namespace HeartwareManagementAPI.DTOs.Discount;

public class DiscountGetFromOrder
{
    public string? DiscountCode { get; set; }

    public string? DiscountName { get; set; }

    public decimal? DiscountValue { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ExpiredDate { get; set; }

    public Guid? CreatedById { get; set; }

}
