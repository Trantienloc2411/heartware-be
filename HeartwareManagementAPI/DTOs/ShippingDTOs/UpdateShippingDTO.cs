namespace HeartwareManagementAPI.DTOs.ShippingDTOs;

public class UpdateShippingDTO
{
    public string ShippingAddress { get; set; }

    public DateTime? StartShipDate { get; set; }

    public DateTime? EndShipDate { get; set; }

    public DateTime? CancelDate { get; set; }
}