using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobile_Store.Data;
using Mobile_Store.Models;
using System.Security.Claims;

namespace Mobile_Store.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
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

            var cartItems = await _db.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            return View(cartItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int productId, int quantity = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var existingItem = await _db.CartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                _db.CartItems.Update(existingItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity
                };
                _db.CartItems.Add(cartItem);
            }

            await _db.SaveChangesAsync();
            TempData["success"] = "Product added to cart!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuantity(int id, int qty)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var item = await _db.CartItems.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
            if (item != null)
            {
                if (qty > 0)
                {
                    item.Quantity = qty;
                    _db.CartItems.Update(item);
                    await _db.SaveChangesAsync();
                    TempData["success"] = "Cart updated!";
                }
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

            var item = await _db.CartItems.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
            if (item != null)
            {
                _db.CartItems.Remove(item);
                await _db.SaveChangesAsync();
                TempData["success"] = "Item removed from cart!";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Clear()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var cartItems = await _db.CartItems.Where(c => c.UserId == userId).ToListAsync();
            _db.CartItems.RemoveRange(cartItems);
            await _db.SaveChangesAsync();
            TempData["success"] = "Cart cleared!";

            return RedirectToAction("Index");
        }
    }
}
