using DotNetWeb.Models;

namespace DotNetWeb.DataAccess.Repository.IRepository
{
  public interface IProductImageRepository : IRepository<ProductImage>
  {
    void Update(ProductImage productImage);
  }
}
