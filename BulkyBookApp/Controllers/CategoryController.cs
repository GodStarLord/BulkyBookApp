using BulkyBookApp.Data;
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
            var objCategoryList = _context.Categories.ToList();
            return View();
        }
    }
}
