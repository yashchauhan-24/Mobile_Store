using System.ComponentModel.DataAnnotations;

namespace Mobile_Store.Models
{
    public class AccessoryReview
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AccessoryId { get; set; }
        public MobileAccessory? Accessory { get; set; }

        [Required]
        public string UserId { get; set; } = null!;
        public ApplicationUser? User { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [StringLength(1000, ErrorMessage = "Comment cannot exceed 1000 characters")]
        public string? Comment { get; set; }

        [StringLength(100)]
        public string? ReviewerName { get; set; }

        public bool IsVerifiedPurchase { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
