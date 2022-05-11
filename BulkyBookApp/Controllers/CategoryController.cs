using BulkyBookApp.Data;
using BulkyBookApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext applicationContext)
        {
            _context = applicationContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _context.Categories;
            return View(categories);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(category);
        }
    }
}
