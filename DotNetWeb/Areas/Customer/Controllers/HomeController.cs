using DotNetWeb.DataAccess.Repository.IRepository;
using DotNetWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DotNetWeb.Areas.Customer.Controllers
{
  [Area("Customer")]
  public class HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork) : Controller
  {
    private readonly ILogger<HomeController> _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public IActionResult Index()
    {
      List<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category,ProductImages");
      return View(productList);
    }

    public IActionResult Details(int productId)
    {
      Product? product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category,ProductImages");
      if (product is null) return NotFound();
      return View(product);
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
