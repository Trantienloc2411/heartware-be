using BusinessObjects.HeartwareENUM;

namespace HeartwareManagementAPI.DTOs.ProductDTO;

public class AddProductDTO
{
    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? UnitsInStock { get; set; }

    public int? CategoryId { get; set; }

    public Enum_ProductStatus? ProductStatus { get; set; }

    public string? ImageUrl { get; set; }
}