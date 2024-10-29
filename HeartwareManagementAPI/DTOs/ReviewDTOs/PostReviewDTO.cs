using BusinessObjects.Entities;

namespace HeartwareManagementAPI.DTOs.ReviewDTOs
{
    public class PostReviewDTO
    {
        public Guid? ProductId { get; set; }

        public int? Rating { get; set; }

        public string? Content { get; set; }

        public Guid? UserId { get; set; }

        public DateTime? CreatedDate { get; set; }

    }
}
