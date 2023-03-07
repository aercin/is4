using System.Linq.Expressions;

namespace core_domain.Abstractions
{
    public interface IGenericRepository<T> where T : class, IAggregateRoot
    {
        T GetById(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Remove(T entity);
    }
}
