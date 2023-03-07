using core_domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace core_infrastructure.persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IAggregateRoot
    {
        private readonly DbContext _dbContext;
        public GenericRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public T GetById(int id)
        {
            return this._dbContext.Set<T>().Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return this._dbContext.Set<T>();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> expression)
        {
            return this._dbContext.Set<T>().Where(expression);
        }

        public void Add(T entity)
        {
            this._dbContext.Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            this._dbContext.Set<T>().Remove(entity);
        }
    }
}
