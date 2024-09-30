using System;
using System.Collections.Generic;
using BusinessObjects.HeartwareENUM;

namespace BusinessObjects.Entities;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid? UserId { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? ConfirmDate { get; set; }

    public Enum_OrderStatus? OrderStatus { get; set; }

    public Enum_PaymentMethod? PaymentMethod { get; set; }

    public int? DiscountId { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public virtual Discount? Discount { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();
}
