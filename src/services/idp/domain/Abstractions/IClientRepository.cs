namespace domain.Abstractions
{
    public interface IClientRepository
    {
        Task<string> GetClientRedirectUrl(string clientId, string redirectUrlType);
    }
}
