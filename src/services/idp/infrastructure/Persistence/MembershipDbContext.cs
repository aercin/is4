using domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Persistence
{
    public class MembershipDbContext : DbContext
    {
        public MembershipDbContext(DbContextOptions<MembershipDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MembershipDbContext).Assembly);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }
}
