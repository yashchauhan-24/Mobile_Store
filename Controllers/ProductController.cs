using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobile_Store.Data;

namespace Mobile_Store.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            var query = _db.Products.Include(p => p.Category).AsQueryable();
            
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
                .FirstOrDefaultAsync(p => p.Id == id);
            
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
