using DotNetWeb.DataAccess.Repository.IRepository;
using DotNetWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetWeb.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class ProductController(IUnitOfWork unitOfWork) : Controller
  {
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public IActionResult Index()
    {
      List<Product> productList = _unitOfWork.Product.GetAll();
      return View(productList);
    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Create(Product obj)
    {
      if (ModelState.IsValid)
      {
        _unitOfWork.Product.Add(obj);
        _unitOfWork.Save();
        TempData["successMessage"] = "Product created successfully";
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

      Product? ProductFromDB = _unitOfWork.Product.Get(u => u.Id == id);
      if (ProductFromDB == null)
      {
        return NotFound();
      }
      return View(ProductFromDB);
    }

    [HttpPost]
    public IActionResult Edit(Product obj)
    {
      if (ModelState.IsValid)
      {
        _unitOfWork.Product.Update(obj);
        _unitOfWork.Save();
        TempData["successMessage"] = "Product updated successfully";
        return RedirectToAction("Index");
      }
      return View();
    }

    [HttpPost]
    public IActionResult Delete(int? id)
    {
      Product? product = _unitOfWork.Product.Get(u => u.Id == id);
      if (product == null)
      {
        return NotFound();
      }
      _unitOfWork.Product.Remove(product);
      _unitOfWork.Save();
      TempData["successMessage"] = "Product deleted successfully";
      return RedirectToAction("Index");
    }
  }
}
