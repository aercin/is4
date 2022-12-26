using core_infrastructure.persistence;
using domain.Abstractions;
using domain.Entities;

namespace infrastructure.Persistence
{
    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(MembershipDbContext context) : base(context)
        {

        }
    }
}
