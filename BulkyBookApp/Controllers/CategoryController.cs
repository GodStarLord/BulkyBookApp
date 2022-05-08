using Microsoft.AspNetCore.Mvc;

namespace BulkyBookApp.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
