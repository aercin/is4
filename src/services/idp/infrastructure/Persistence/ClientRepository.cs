using domain.Abstractions;
using IdentityServer4.Stores;

namespace infrastructure.Persistence
{
    public class ClientRepository : IClientRepository
    {
        private readonly IClientStore _clientStore;
        public ClientRepository(IClientStore clientStore)
        {
            this._clientStore = clientStore;
        }

        public async Task<string> GetClientRedirectUrl(string clientId, string redirectUrlType)
        {
            var relatedClient = await this._clientStore.FindClientByIdAsync(clientId);
            
            return relatedClient.RedirectUris.Single(x => x.Contains(redirectUrlType));
        }
    }
}
