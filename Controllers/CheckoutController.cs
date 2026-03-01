using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobile_Store.Data;
using Mobile_Store.Models;
using System.Security.Claims;

namespace Mobile_Store.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CheckoutController(ApplicationDbContext db)
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

            var items = await _db.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();
            
            if (!items.Any())
            {
                TempData["error"] = "Your cart is empty!";
                return RedirectToAction("Index", "Cart");
            }

            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var items = await _db.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();
            
            if (!items.Any())
            {
                TempData["error"] = "Your cart is empty!";
                return RedirectToAction("Index", "Cart");
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Status = "Processing",
                TotalAmount = items.Sum(i => i.Product!.Price * i.Quantity),
                Items = new List<OrderItem>()
            };

            foreach (var it in items)
            {
                order.Items!.Add(new OrderItem 
                { 
                    ProductId = it.ProductId, 
                    Quantity = it.Quantity, 
                    UnitPrice = it.Product!.Price 
                });
            }

            _db.Orders.Add(order);
            _db.CartItems.RemoveRange(items);
            await _db.SaveChangesAsync();

            TempData["success"] = "Order placed successfully!";
            return RedirectToAction("Success", new { id = order.Id });
        }

        public async Task<IActionResult> Success(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var order = await _db.Orders
                .Include(o => o.Items!)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);
            
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public async Task<IActionResult> Orders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var orders = await _db.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Items!)
                .ThenInclude(i => i.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            
            return View(orders);
        }
    }
}
