using BulkyBook.DataAccess.Repositories.Interfaces;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            return View();
        }

        //// GET
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(CoverType coverType)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.CoverType.Add(coverType);
        //        _unitOfWork.Save();

        //        TempData["success"] = "Cover Type Created Successfully!";

        //        return RedirectToAction("Index");
        //    }

        //    return View(coverType);
        //}

        // GET
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category
                    .GetAll()
                    .Select(x => new SelectListItem(x.Name, x.Id.ToString())),
                CoverTypeList = _unitOfWork.CoverType
                    .GetAll()
                    .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            };

            if (id == null || id == 0)
            {
                // Create Product

                // One Way Binding from Controller to View.
                // Life Cycle till that HTTP Request, values are lost if redirection occurs
                // return View(product); => ViewBag is also passed to the View
                //ViewBag.categoryList = categoryList;
                //ViewData["coverTypeList"] = coverTypeList;

                return View(productVM);
            }
            else
            {
                // Update Product
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
                return View(productVM);
            }
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM, IFormFile? formFile)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (formFile != null)
                {
                    // Copy file into wwwroot/images/products

                    string fileName = Guid.NewGuid().ToString();
                    var uploadLocation = Path.Combine(wwwRootPath, @"images\products");
                    var fileExtension = Path.GetExtension(formFile.FileName);

                    // Image Exists => Delete Old Image and Copy New Image
                    if (productVM.Product.ImageURL != null)
                    {
                        var oldImage = Path.Combine(wwwRootPath, productVM.Product.ImageURL.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploadLocation, fileName + fileExtension), FileMode.Create))
                    {
                        formFile.CopyTo(fileStream);
                        // Copy done
                    }

                    productVM.Product.ImageURL = @"\images\products\" + fileName + fileExtension;
                }

                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
                _unitOfWork.Save();

                TempData["success"] = "Product Created Successfully!";

                return RedirectToAction("Index");
            }

            return View(productVM);
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

        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            var productsList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return Json(new { data = productsList });
        }

        #endregion
    }
}
