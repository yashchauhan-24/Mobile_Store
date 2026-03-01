using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobile_Store.Data;
using Mobile_Store.Models;

namespace Mobile_Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var featured = await _db.Products.Include(p => p.Category).Take(8).ToListAsync();
            var categories = await _db.Categories.ToListAsync();
            ViewBag.Categories = categories;
            return View(featured);
        }

        public IActionResult About()
        {
            return View();
        }

        public async Task<IActionResult> Category(int id)
        {
            var products = await _db.Products.Where(p => p.CategoryId == id).Include(p => p.Category).ToListAsync();
            var categories = await _db.Categories.ToListAsync();
            ViewBag.Categories = categories;
            return View(products);
        }

        public async Task<IActionResult> Search(string q)
        {
            var products = await _db.Products.Where(p => p.Name.Contains(q)).Include(p => p.Category).ToListAsync();
            ViewBag.Query = q;
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}
