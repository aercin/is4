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
            var persistedGrant = this._persistentGrantDbContext.PersistedGrants.SingleOrDefault(x => x.ClientId == clientId
                                                                                                  && x.SubjectId == userId.ToString());
            if (persistedGrant != null)
            {
                this._persistentGrantDbContext.PersistedGrants.Remove(persistedGrant);
                await this._persistentGrantDbContext.SaveChangesAsync();
            }
        }
    }
}
