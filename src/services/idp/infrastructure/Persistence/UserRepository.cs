using core_infrastructure.persistence;
using domain.Abstractions;
using domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Persistence
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly MembershipDbContext _context;
        public UserRepository(MembershipDbContext context) : base(context)
        {
            this._context = context;
        }

        public User? GetWithPermissions(string userName, string password)
        {
            return this._context.Users.Include(x => x.Permissions).ThenInclude(x => x.Permission).SingleOrDefault(x => x.UserName == userName && x.Password == password);
        }
    }
}
