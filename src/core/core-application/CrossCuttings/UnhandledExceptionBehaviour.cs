using core_application.Abstractions;
using core_application.Common;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace core_application.Features.CrossCuttings
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
                                                                                                           where TResponse : Result
    {
        private readonly ILogger<TRequest> _logger;
        private readonly IHttpContextService _httpContextService;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger, IHttpContextService httpContextService)
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

            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                string errorMessage = "Sistemde bekleyen bir durum oluştu.";
                if (ex.GetType() == typeof(ValidationException))
                {
                    errorMessage = "Yanlış veya eksik bilgi gönderimi söz konusudur.";
                }

                this._logger.LogError(ex, $"TrackId:{this._httpContextService.CorrelationId} {errorMessage}");

                return (TResponse)Result.Failure(new List<string> { errorMessage });
            }
        }
    }
}
