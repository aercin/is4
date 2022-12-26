namespace core_domain.Abstractions
{
    public interface IBaseUnitOfWork : IDisposable
    {
        Task CompleteAsync(); 
    }
}
