using DotNetWeb.DataAccess.Repository.IRepository;
using DotNetWeb.Models;
using DotNetWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotNetWeb.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) : Controller
  {
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

    [NonAction]
    private List<SelectListItem> GetCategorySelectList()
    {
      return _unitOfWork.Category.GetAll().Select(u => new SelectListItem
      {
        Text = u.Name,
        Value = u.Id.ToString()
      }).ToList();
    }

    [NonAction]
    private void SaveProductImages(Product product, List<IFormFile> files)
    {
      string wwwRootPath = _webHostEnvironment.WebRootPath;
      product.ProductImages ??= new List<ProductImage>();

      string productPath = Path.Combine("images", "products", $"product-{product.Id}");
      string finalPath = Path.Combine(wwwRootPath, productPath);

      if (!Directory.Exists(finalPath)) Directory.CreateDirectory(finalPath);

      foreach (IFormFile file in files)
      {
        string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        string filePath = Path.Combine(finalPath, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
          file.CopyTo(fileStream);
        }

        product.ProductImages.Add(new ProductImage
        {
          ImageUrl = @"\" + productPath + @"\" + fileName,
          ProductId = product.Id
        });
      }
    }

    public IActionResult Index()
    {
      List<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
      return View(productList);
    }

    public IActionResult Upsert(int? id)
    {
      ProductVM productVM = new()
      {
        CategoryList = GetCategorySelectList(),
        Product = new Product()
      };

      if (id == null || id == 0) return View(productVM);

      var product = _unitOfWork.Product.Get(u => u.Id == id, includeProperties: "ProductImages");
      if (product == null) return NotFound();
      productVM.Product = product;
      return View(productVM);
    }

    [HttpPost]
    public IActionResult Upsert(ProductVM productVM, List<IFormFile> files)
    {
      if (ModelState.IsValid)
      {
        bool isNew = productVM.Product.Id == 0;
        if (isNew)
        {
          _unitOfWork.Product.Add(productVM.Product);
          _unitOfWork.Save();
        }
        else
        {
          _unitOfWork.Product.Update(productVM.Product);
        }

        if (files != null && files.Count > 0)
        {
          SaveProductImages(productVM.Product, files);
          _unitOfWork.Product.Update(productVM.Product);
        }

        _unitOfWork.Save();

        TempData["success"] = "Product created/updated successfully";
        return RedirectToAction("Index");
      }

      productVM.CategoryList = GetCategorySelectList();
      return View(productVM);
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
