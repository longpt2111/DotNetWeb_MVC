using DotNetWeb.DataAccess.Repository.IRepository;
using DotNetWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetWeb.Controllers
{
    public class CategoryController(ICategoryRepository categoryRepo) : Controller
    {
        private readonly ICategoryRepository _categoryRepo = categoryRepo;
        public IActionResult Index()
        {
            List<Category> categoryList = _categoryRepo.GetAll();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _categoryRepo.Add(obj);
                _categoryRepo.Save();
                TempData["successMessage"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Update(obj);
                _categoryRepo.Save();
                TempData["successMessage"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            Category? category = _categoryRepo.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(category);
            _categoryRepo.Save();
            TempData["successMessage"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
