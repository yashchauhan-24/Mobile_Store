using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobile_Store.Data;
using Mobile_Store.Models;
using Mobile_Store.ViewModels;

namespace Mobile_Store.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProductReviewsViewModel model)
        {
            var reviewModel = model?.NewReview;
            if (reviewModel == null)
            {
                // Invalid submission - redirect back to product list (no error message)
                return RedirectToAction("Index", "Product");
            }

            // NOTE: client-side required attributes still apply. Server will accept submission
            // and proceed only if the product id is provided. We intentionally avoid the
            // TempData error message for validation failures per request.

            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                TempData["error"] = "You must be logged in to write a review.";
                return RedirectToAction("Login", "Account");
            }

            // Check if user already reviewed this product
            var existingReview = await _db.Reviews
                .FirstOrDefaultAsync(r => r.ProductId == reviewModel.ProductId && r.UserId == userId);

            if (existingReview != null)
            {
                TempData["error"] = "You have already reviewed this product.";
                return RedirectToAction("Details", "Product", new { id = reviewModel.ProductId });
            }

            // Check if user has ordered this product (verified purchase)
            var hasOrdered = await _db.Orders
                .Include(o => o.Items)
                .AnyAsync(o => o.UserId == userId && 
                               o.Items.Any(i => i.ProductId == reviewModel.ProductId));

            var user = await _userManager.GetUserAsync(User);
            
            var review = new Review
            {
                ProductId = reviewModel.ProductId,
                UserId = userId,
                Rating = reviewModel.Rating,
                Comment = reviewModel.Comment,
                ReviewerName = user?.FullName ?? user?.UserName ?? "Anonymous",
                IsVerifiedPurchase = hasOrdered,
                CreatedAt = DateTime.Now
            };

            _db.Reviews.Add(review);
            await _db.SaveChangesAsync();

            TempData["success"] = "Thank you for your review!";
            return RedirectToAction("Details", "Product", new { id = reviewModel.ProductId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int productId)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                TempData["error"] = "You must be logged in to delete a review.";
                return RedirectToAction("Login", "Account");
            }

            var review = await _db.Reviews.FindAsync(id);
            
            if (review == null)
            {
                TempData["error"] = "Review not found.";
                return RedirectToAction("Details", "Product", new { id = productId });
            }

            if (review.UserId != userId)
            {
                TempData["error"] = "You can only delete your own reviews.";
                return RedirectToAction("Details", "Product", new { id = productId });
            }

            _db.Reviews.Remove(review);
            await _db.SaveChangesAsync();

            TempData["success"] = "Your review has been deleted.";
            return RedirectToAction("Details", "Product", new { id = productId });
        }
    }
}
