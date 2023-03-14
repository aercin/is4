namespace core_application.Abstractions
{
    public interface IHttpContextService
    {
        string CorrelationId { get; }

        string GetClaimValue(string claimType);

        bool isClaimExist(string claimValue);
    }
}
