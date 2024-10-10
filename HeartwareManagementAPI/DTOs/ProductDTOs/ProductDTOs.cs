namespace HeartwareManagementAPI.DTOs.ProductDTO;

public class ProductDTOs
{
    public Guid ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? UnitsInStock { get; set; }

    public int? CategoryId { get; set; }
    public string CategoryName { get; set; }

    public int? ProductStatus { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? ImageUrl { get; set; }
}