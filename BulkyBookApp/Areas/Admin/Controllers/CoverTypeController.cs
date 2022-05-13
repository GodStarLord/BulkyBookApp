using BulkyBook.DataAccess.Repositories.Interfaces;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        
        public IActionResult Index()
        {
            IEnumerable<CoverType> coverTypeList = _unitOfWork.CoverType.GetAll();
            return View(coverTypeList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(coverType);
                _unitOfWork.Save();

                TempData["success"] = "Cover Type Created Successfully!";

                return RedirectToAction("Index");
            }

            return View(coverType);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var coverType = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);
            //var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            //var category = _context.Categories.SingleOrDefault(c => c.Id == id);

            if (coverType == null)
            {
                return NotFound();
            }

            return View(coverType);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(coverType);
                _unitOfWork.Save();

                TempData["success"] = "Cover Type Updated Successfully!";

                return RedirectToAction("Index");
            }

            return View(coverType);
        }

        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var coverType = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
            //var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            //var category = _context.Categories.SingleOrDefault(c => c.Id == id);

            if (coverType == null)
            {
                return NotFound();
            }

            return View(coverType);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var coverType = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);

            if (coverType == null)
            {
                return NotFound();
            }

            _unitOfWork.CoverType.Remove(coverType);
            _unitOfWork.Save();

            TempData["success"] = "Cover Type Deleted Successfully!";

            return RedirectToAction("Index");
        }
    }
}
