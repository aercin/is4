using core_infrastructure.persistence;
using domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure.Persistence
{
    public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(MembershipDbContext context, IServiceProvider serviceProvider) : base(context)
        {
            this._serviceProvider = serviceProvider;
        }

        public IUserRepository UserRepo
        {
            get
            {
                return this._serviceProvider.GetRequiredService<IUserRepository>();
            }
        }

        public IPermissionRepository PermissionRepo
        {
            get
            {
                return this._serviceProvider.GetRequiredService<IPermissionRepository>();
            }
        }
    }
}
