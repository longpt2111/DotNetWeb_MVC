using DotNetWeb.DataAccess.Data;
using DotNetWeb.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DotNetWeb.DataAccess.Repository
{
  public class Repository<T> : IRepository<T> where T : class
  {
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;

    public Repository(ApplicationDbContext db)
    {
      _db = db;
      dbSet = _db.Set<T>();
    }

    private IQueryable<T> ApplyIncludeProperties(IQueryable<T> query, string? includeProperties)
    {
      if (!string.IsNullOrEmpty(includeProperties))
      {
        foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
          query = query.Include(includeProp);
        }
      }
      return query;
    }

    public void Add(T entity)
    {
      dbSet.Add(entity);
    }

    public T? Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
    {
      IQueryable<T> query = tracked ? dbSet : dbSet.AsNoTracking();
      query = query.Where(filter);
      return ApplyIncludeProperties(query, includeProperties).FirstOrDefault();
    }

    public List<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
    {
      IQueryable<T> query = dbSet;
      if (filter is not null)
      {
        query = query.Where(filter);
      }
      return ApplyIncludeProperties(query, includeProperties).ToList();
    }

    public void Remove(T entity)
    {
      dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
      dbSet.RemoveRange(entities);
    }
  }
}
