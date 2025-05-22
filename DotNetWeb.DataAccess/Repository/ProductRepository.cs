using DotNetWeb.DataAccess.Data;
using DotNetWeb.DataAccess.Repository.IRepository;
using DotNetWeb.Models;

namespace DotNetWeb.DataAccess.Repository
{
  public class ProductRepository : Repository<Product>, IProductRepository
  {
    private readonly ApplicationDbContext _db;

    public ProductRepository(ApplicationDbContext db) : base(db)
    {
      _db = db;
    }

    public void Update(Product product)
    {
      _db.Products.Update(product);
    }
  }
}
