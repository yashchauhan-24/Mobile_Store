using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobile_Store.Data;
using Mobile_Store.ViewModels;
using Microsoft.AspNetCore.Identity;
using Mobile_Store.Models;

namespace Mobile_Store.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            var query = _db.Products.Include(p => p.Category).Include(p => p.Reviews).AsQueryable();
            
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
                var category = await _db.Categories.FindAsync(categoryId.Value);
                ViewBag.CategoryName = category?.Name;
            }

            var products = await query.ToListAsync();
            var categories = await _db.Categories.ToListAsync();
            ViewBag.Categories = categories;
            
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _db.Products
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                    .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(p => p.Id == id);
            
            if (product == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            
            // Get reviews
            var reviews = product.Reviews?
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new ReviewViewModel
                {
                    Id = r.Id,
                    ReviewerName = r.ReviewerName,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt,
                    IsVerifiedPurchase = r.IsVerifiedPurchase,
                    UserId = r.UserId
                })
                .ToList() ?? new List<ReviewViewModel>();

            // Calculate rating distribution
            var ratingDistribution = new Dictionary<int, int>();
            for (int i = 1; i <= 5; i++)
            {
                ratingDistribution[i] = reviews.Count(r => r.Rating == i);
            }

            // Check if user has reviewed
            bool userHasReviewed = false;
            bool userHasOrdered = false;

            if (!string.IsNullOrEmpty(userId))
            {
                userHasReviewed = reviews.Any(r => r.UserId == userId);
                
                // Check if user has ordered this product
                userHasOrdered = await _db.Orders
                    .Include(o => o.Items)
                    .AnyAsync(o => o.UserId == userId && 
                                   o.Items.Any(i => i.ProductId == id));
            }

            var viewModel = new ProductReviewsViewModel
            {
                Product = product,
                Reviews = reviews,
                AverageRating = product.AverageRating,
                RatingDistribution = ratingDistribution,
                UserHasReviewed = userHasReviewed,
                UserHasOrdered = userHasOrdered,
                NewReview = new AddReviewViewModel { ProductId = id }
            };

            return View(viewModel);
        }
    }
}
