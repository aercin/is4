using application.Abstractions;
using application.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace application.Features.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
                                                                                                where TResponse : Result
    {
        private readonly ILogger<TRequest> _logger;
        private readonly ICorrelationService _correlationService;

        public LoggingBehaviour(ILogger<TRequest> logger, ICorrelationService correlationService)
        {
            this._logger = logger;
            this._correlationService = correlationService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            this._logger.LogInformation($"TrackId: {this._correlationService.CorrelationId} Request: {JsonConvert.SerializeObject(request)}");

            var response = await next();

            this._logger.LogInformation($"TrackId: {this._correlationService.CorrelationId} Response: {JsonConvert.SerializeObject(response)}");

            return response;
        }
    }
}
