using System;

namespace HeartwareManagementAPI.DTOs;

public class OrderDetailsDto
{
    public Guid? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? Subtotal { get; set; }
}
