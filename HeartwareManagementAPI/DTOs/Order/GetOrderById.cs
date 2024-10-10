using System;
using HeartwareManagementAPI.DTOs.Discount;

namespace HeartwareManagementAPI.DTOs.Order;

public class GetOrderById
{
    public Guid OrderId { get; set; }

    public Guid? UserId { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? ConfirmDate { get; set; }

    public int? OrderStatus { get; set; }

    public int? PaymentMethod { get; set; }

    public int? DiscountId { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public DiscountGetFromOrder? Discount {get; set;}

    public List<OrderDetailsDto>? OrderDetails { get; set; }

}

