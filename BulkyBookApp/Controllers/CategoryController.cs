﻿using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repositories.Implementations;
using BulkyBook.DataAccess.Repositories.Interfaces;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _context;

        public CategoryController(ICategoryRepository context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _context.GetAll();
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
                _context.Add(category);
                _context.Save();

                TempData["success"] = "Category Created Successfully!";

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

            var category = _context.GetFirstOrDefault(x => x.Id == id);
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
                _context.Update(category);
                _context.Save();

                TempData["success"] = "Category Updated Successfully!";

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

            var category = _context.GetFirstOrDefault(c => c.Id == id);
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
            var category = _context.GetFirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            _context.Remove(category);
            _context.Save();

            TempData["success"] = "Category Deleted Successfully!";

            return RedirectToAction("Index");
        }
    }
}
