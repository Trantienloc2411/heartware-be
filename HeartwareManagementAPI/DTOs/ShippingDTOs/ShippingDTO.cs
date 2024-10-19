namespace HeartwareManagementAPI.DTOs.ShippingDTOs;

public class ShippingDTO
{
    public Guid ShippingId { get; set; }

    public Guid? OrderId { get; set; }

    public string? ShippingAddress { get; set; }

    public DateTime? StartShipDate { get; set; }

    public DateTime? EndShipDate { get; set; }

    public DateTime? CancelDate { get; set; }

    public string? TrackingNumber { get; set; }
}