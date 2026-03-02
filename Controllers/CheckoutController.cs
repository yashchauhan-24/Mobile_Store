using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobile_Store.Data;
using Mobile_Store.Models;
using Mobile_Store.ViewModels;
using System.Security.Claims;

namespace Mobile_Store.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public CheckoutController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
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

            // Pre-fill user information
            var user = await _userManager.GetUserAsync(User);
            var model = new CheckoutViewModel
            {
                CustomerName = user?.FullName ?? "",
                CustomerPhone = user?.PhoneNumber ?? "",
                ShippingAddress = !string.IsNullOrEmpty(user?.Address) 
                    ? $"{user.Address}, {user.City}, {user.State} {user.ZipCode}, {user.Country}"
                    : "",
                PaymentMethod = "Cash on Delivery"
            };

            ViewBag.CartItems = items;
            ViewBag.Total = items.Sum(i => i.Product!.Price * i.Quantity);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(CheckoutViewModel model)
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

            // Validate payment method specific fields
            if (model.PaymentMethod == "Online Payment")
            {
                if (string.IsNullOrEmpty(model.CardNumber) || 
                    string.IsNullOrEmpty(model.CardHolderName) ||
                    !model.ExpiryMonth.HasValue || 
                    !model.ExpiryYear.HasValue ||
                    string.IsNullOrEmpty(model.CVV))
                {
                    ModelState.AddModelError("", "Please provide all card details for online payment.");
                    ViewBag.CartItems = items;
                    ViewBag.Total = items.Sum(i => i.Product!.Price * i.Quantity);
                    return View("Index", model);
                }

                // Simple card validation
                if (model.CardNumber.Length < 13 || model.CardNumber.Length > 16)
                {
                    ModelState.AddModelError("CardNumber", "Invalid card number.");
                    ViewBag.CartItems = items;
                    ViewBag.Total = items.Sum(i => i.Product!.Price * i.Quantity);
                    return View("Index", model);
                }

                if (model.CVV.Length < 3 || model.CVV.Length > 4)
                {
                    ModelState.AddModelError("CVV", "Invalid CVV.");
                    ViewBag.CartItems = items;
                    ViewBag.Total = items.Sum(i => i.Product!.Price * i.Quantity);
                    return View("Index", model);
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.CartItems = items;
                ViewBag.Total = items.Sum(i => i.Product!.Price * i.Quantity);
                return View("Index", model);
            }

            // Create order
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Status = "Processing",
                TotalAmount = items.Sum(i => i.Product!.Price * i.Quantity),
                PaymentMethod = model.PaymentMethod,
                PaymentStatus = model.PaymentMethod == "Cash on Delivery" ? "Pending" : "Paid",
                TransactionId = model.PaymentMethod == "Online Payment" 
                    ? "TXN" + DateTime.Now.Ticks.ToString() 
                    : null,
                CustomerName = model.CustomerName,
                CustomerPhone = model.CustomerPhone,
                ShippingAddress = model.ShippingAddress,
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

            TempData["success"] = model.PaymentMethod == "Online Payment" 
                ? "Payment successful! Order placed." 
                : "Order placed successfully! Pay on delivery.";
            
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
