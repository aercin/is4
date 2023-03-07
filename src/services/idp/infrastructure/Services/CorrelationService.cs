using application.Abstractions;
using Microsoft.AspNetCore.Http;

namespace infrastructure.Services
{
    public class CorrelationService : ICorrelationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CorrelationService(IHttpContextAccessor httpContextAccessor)
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
    }
}
