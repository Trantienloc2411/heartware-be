namespace HeartwareManagementAPI.DTOs.ReviewDTOs;

public class ReviewResponseDTO
{
    public int ReviewId { get; set; }
    public Guid? ProductId { get; set; }
    public string? Content { get; set; }
    public int? Rating { get; set; }
    public DateTime? ReviewCreatedDate { get; set; }

}