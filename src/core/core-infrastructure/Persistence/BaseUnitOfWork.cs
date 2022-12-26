using core_domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace core_infrastructure.persistence
{
    public class BaseUnitOfWork : IBaseUnitOfWork
    {
        private readonly DbContext _context;

        public BaseUnitOfWork(DbContext context)
        {
            this._context = context;
        }
        public async Task CompleteAsync()
        {
            await this._context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}
