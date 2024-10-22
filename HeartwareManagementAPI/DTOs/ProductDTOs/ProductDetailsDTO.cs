namespace HeartwareManagementAPI.DTOs.ProductDTO;

public class ProductDetailsDTO
{
    public int? ProductDetailId { get; set; }

    public string? ProductParam { get; set; }

    public string? ProductValue { get; set; }

    public Guid? ProductId { get; set; }
    
    public string ProductName { get; set; }
}