namespace domain.Abstractions
{
    public interface IPersistentGrantRepository
    {
        Task RemovePersistedGrant(string clientId, int userId);
    }
}
