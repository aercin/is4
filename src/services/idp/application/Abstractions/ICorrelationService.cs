namespace application.Abstractions
{
    public interface ICorrelationService
    {
        string CorrelationId { get; }
    }
}
