using domain.Abstractions;
using IdentityServer4.EntityFramework.Interfaces;

namespace infrastructure.Persistence
{
    public class PersistedGrantRepository : IPersistentGrantRepository
    {
        private readonly IPersistedGrantDbContext _persistentGrantDbContext;

        public PersistedGrantRepository(IPersistedGrantDbContext persistentGrantDbContext)
        {
            this._persistentGrantDbContext = persistentGrantDbContext;
        }

        public async Task RemovePersistedGrant(string clientId, int userId)
        {
            var persistedGrants = this._persistentGrantDbContext.PersistedGrants.Where(x => x.ClientId == clientId
                                                                                        && x.SubjectId == userId.ToString());
            if (persistedGrants.Any())
            {
                this._persistentGrantDbContext.PersistedGrants.RemoveRange(persistedGrants);
                await this._persistentGrantDbContext.SaveChangesAsync();
            }
        }
    }
}
