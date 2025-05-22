using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotNetWeb.Models.ViewModels
{
  public class ProductVM
  {
    public Product Product { get; set; }

    [ValidateNever]
    public List<SelectListItem> CategoryList { get; set; }
  }
}
