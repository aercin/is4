using core_application.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace core_infrastructure.Services
{
    public class HttpContextService : IHttpContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public string CorrelationId
        {
            get
            {
                return this._httpContextAccessor.HttpContext.Request.Headers["TrackId"];
            }
        }

        public string GetClaimValue(string claimType)
        {
            return this._httpContextAccessor.HttpContext.User.Claims.Single(x => x.Type == claimType).Value;
        }

        public bool isClaimExist(string claimValue)
        {
            return this._httpContextAccessor.HttpContext.User.Claims.Any(x => x.Value.ToLower() == claimValue.ToLower());
        }
    }
}
