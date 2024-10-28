using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessObjects.Entities;

public partial class Order
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
    [JsonIgnore]
    public virtual Discount? Discount { get; set; }
    [JsonIgnore]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
