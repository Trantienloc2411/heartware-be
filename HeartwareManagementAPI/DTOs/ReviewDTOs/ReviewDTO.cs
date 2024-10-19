namespace HeartwareManagementAPI.DTOs.ReviewDTOs;

public class ReviewDTO
{
    public int ReviewId { get; set; }
    public Guid? ProductId { get; set; }
    public string ProductName { get; set; } 
    public string? Content { get; set; }
    public int? Rating { get; set; }
    public DateTime? CreatedDate { get; set; }

}