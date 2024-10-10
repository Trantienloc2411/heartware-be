using System;

namespace HeartwareManagementAPI.DTOs.Order;

public class UpdateOrder
{
    public Guid? Id { get; set;}
    public Guid? UserId {get;set;}
    public DateTime? ConfirmDate {get; set;}    
    public int? OrderStatus {get;set;}
}
