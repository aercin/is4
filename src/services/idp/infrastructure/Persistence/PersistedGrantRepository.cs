using domain.Abstractions;
using IdentityServer4.Stores;

namespace infrastructure.Persistence
{
    public class PersistedGrantRepository : IPersistentGrantRepository
    {
        private readonly IPersistedGrantStore _persistedGrantStore;
        public PersistedGrantRepository(IPersistedGrantStore persistedGrantStore)
        {
            this._persistedGrantStore = persistedGrantStore;
        }

        public async Task RemovePersistedGrant(string clientId, int userId)
        {
            var persistentGrants = await this._persistedGrantStore.GetAllAsync(new PersistedGrantFilter { ClientId = clientId, SubjectId = userId.ToString() });

            if (persistentGrants.Any())
            {
                await this._persistedGrantStore.RemoveAsync(persistentGrants.Single().Key);
            }
        }
    }
}
