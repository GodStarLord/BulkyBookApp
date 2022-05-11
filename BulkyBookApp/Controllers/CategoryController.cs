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
            if (category.Name.Equals(category.DisplayOrder.ToString()))
            {
                ModelState.AddModelError("Name", "Name and DisplayOrder cannot be same!");
            }

            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _context.Categories.Find(id);
            //var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            //var category = _context.Categories.SingleOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.Name.Equals(category.DisplayOrder.ToString()))
            {
                ModelState.AddModelError("Name", "Name and DisplayOrder cannot be same!");
            }

            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _context.Categories.Find(id);
            //var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            //var category = _context.Categories.SingleOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
