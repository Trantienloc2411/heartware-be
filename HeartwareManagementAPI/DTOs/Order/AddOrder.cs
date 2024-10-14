using System;
using BusinessObjects.Entities;

namespace HeartwareManagementAPI.DTOs.Order;

public class AddOrder
{
    public int? PaymentMethod { get; set; }

    public int? DiscountId { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    

    public virtual ICollection<AddOrderDetail> OrderDetails { get; set; } = new List<AddOrderDetail>();
}
public class AddOrderDetail 
{
    public Guid? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? Subtotal { get; set; }

}
