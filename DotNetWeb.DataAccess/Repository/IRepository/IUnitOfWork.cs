﻿namespace DotNetWeb.DataAccess.Repository.IRepository
{
  public interface IUnitOfWork
  {
    ICategoryRepository Category { get; }
    IProductRepository Product { get; }
    IProductImageRepository ProductImage { get; }

    void Save();
  }
}
