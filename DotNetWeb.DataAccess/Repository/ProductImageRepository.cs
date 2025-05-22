﻿using DotNetWeb.DataAccess.Data;
using DotNetWeb.DataAccess.Repository.IRepository;
using DotNetWeb.Models;

namespace DotNetWeb.DataAccess.Repository
{
  public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
  {
    private readonly ApplicationDbContext _db;

    public ProductImageRepository(ApplicationDbContext db) : base(db)
    {
      _db = db;
    }

    public void Update(ProductImage productImage)
    {
      _db.ProductImages.Update(productImage);
    }
  }
}
