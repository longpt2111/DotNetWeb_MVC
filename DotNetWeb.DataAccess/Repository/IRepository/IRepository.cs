using System.Linq.Expressions;

namespace DotNetWeb.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T? Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Save();
    }
}
