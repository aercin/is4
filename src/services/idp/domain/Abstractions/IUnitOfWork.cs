using core_domain.Abstractions;

namespace domain.Abstractions
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        IUserRepository UserRepo { get; }
        IPermissionRepository PermissionRepo { get; }
    }
}
