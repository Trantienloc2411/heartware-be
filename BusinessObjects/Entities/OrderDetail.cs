using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessObjects.Entities;

public partial class OrderDetail
{
    public Guid OrderDetailId { get; set; }

    public Guid? OrderId { get; set; }

    public Guid? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? Subtotal { get; set; }
    [JsonIgnore]
    public virtual Order? Order { get; set; }
}
