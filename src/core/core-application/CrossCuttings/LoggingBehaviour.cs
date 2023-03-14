using core_application.Abstractions;
using core_application.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace core_application.Features.CrossCuttings
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
                                                                                                where TResponse : Result
    {
        private readonly ILogger<TRequest> _logger;
        private readonly IHttpContextService _httpContextService;
        public LoggingBehaviour(ILogger<TRequest> logger, IHttpContextService httpContextService)
        {
            this._logger = logger;
            this._httpContextService = httpContextService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (CrossCuttingHelper.ShouldSkip<TRequest>())
            {
                return await next();
            }

            this._logger.LogInformation($"TrackId: {this._httpContextService.CorrelationId} Request: {JsonConvert.SerializeObject(request)}");

            var response = await next();

            this._logger.LogInformation($"TrackId: {this._httpContextService.CorrelationId} Response: {JsonConvert.SerializeObject(response)}");

            return response;
        }
    }
}
