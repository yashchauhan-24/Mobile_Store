using Mobile_Store.Models;

namespace Mobile_Store.ViewModels
{
    public class ProductReviewsViewModel
    {
        public Product Product { get; set; } = null!;
        public List<ReviewViewModel> Reviews { get; set; } = new();
        public AddReviewViewModel NewReview { get; set; } = new();
        public double AverageRating { get; set; }
        public Dictionary<int, int> RatingDistribution { get; set; } = new();
        public bool UserHasReviewed { get; set; }
        public bool UserHasOrdered { get; set; }
    }
}
