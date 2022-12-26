using core_domain.Abstractions;
using domain.Entities;

namespace domain.Abstractions
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User? GetWithPermissions(string userName, string password);
    }
}
