using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessObjects.Entities;

public partial class Shipping
{
    public Guid ShippingId { get; set; }

    public Guid? OrderId { get; set; }

    public string? ShippingAddress { get; set; }

    public DateTime? StartShipDate { get; set; }

    public DateTime? EndShipDate { get; set; }

    public DateTime? CancelDate { get; set; }

    public string? TrackingNumber { get; set; }
    [JsonIgnore]
    public virtual Order? Order { get; set; }   
}
