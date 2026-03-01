using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobile_Store.Data;
using Mobile_Store.Models;
using System.Security.Claims;

namespace Mobile_Store.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly ApplicationDbContext _db;

        public WishlistController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var items = await _db.Wishlists
                .Where(w => w.UserId == userId)
                .Include(w => w.Product)
                .ThenInclude(p => p.Category)
                .ToListAsync();
            
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var existing = await _db.Wishlists
                .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);
            
            if (existing == null)
            {
                _db.Wishlists.Add(new Wishlist { UserId = userId, ProductId = productId });
                await _db.SaveChangesAsync();
                TempData["success"] = "Product added to wishlist!";
            }
            else
            {
                TempData["error"] = "Product is already in your wishlist!";
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var item = await _db.Wishlists
                .FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);
            
            if (item != null)
            {
                _db.Wishlists.Remove(item);
                await _db.SaveChangesAsync();
                TempData["success"] = "Product removed from wishlist!";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var existing = await _db.Wishlists
                .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);
            
            if (existing != null)
            {
                _db.Wishlists.Remove(existing);
                TempData["success"] = "Product removed from wishlist!";
            }
            else
            {
                _db.Wishlists.Add(new Wishlist { UserId = userId, ProductId = productId });
                TempData["success"] = "Product added to wishlist!";
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
