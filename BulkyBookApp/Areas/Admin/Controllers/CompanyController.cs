using BulkyBook.DataAccess.Repositories.Interfaces;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Company company = new();

            if (id == null || id == 0)
            {
                // Create Product

                return View(company);
            }
            else
            {
                // Update Product
                company = _unitOfWork.Company.GetFirstOrDefault(x => x.Id == id);
                
                return View(company);
            }
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {
                    _unitOfWork.Company.Add(company);

                    TempData["success"] = "Product Created Successfully!";
                }
                else
                {
                    _unitOfWork.Company.Update(company);

                    TempData["success"] = "Product Updated Successfully!";
                }
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(company);
        }

        

        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.Company.GetAll();
            return Json(new { data = companyList });
        }

        // POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyFromDB = _unitOfWork.Company.GetFirstOrDefault(c => c.Id == id);

            if (companyFromDB == null)
            {
                return Json(new { success = "false", message = "Error while fetching records!" });
            }

            _unitOfWork.Company.Remove(companyFromDB);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Company Deleted Successfully!" });
        }

        #endregion
    }
}
