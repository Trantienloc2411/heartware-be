namespace HeartwareManagementAPI.DTOs.ShippingDTOs;

public class AddShippingDTO
{
    public Guid? OrderId { get; set; }
    public DateTime? StartShipDate { get; set; }

    public DateTime? EndShipDate { get; set; }

    public DateTime? CancelDate { get; set; }

    public virtual BusinessObjects.Entities.Order? Order { get; set; }  
}