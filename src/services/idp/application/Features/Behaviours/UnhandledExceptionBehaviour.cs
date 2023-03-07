using application.Abstractions;
using application.Common;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace application.Features.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
                                                                                                           where TResponse : Result
    {
        private readonly ILogger<TRequest> _logger;
        private readonly ICorrelationService _correlationService;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger, ICorrelationService correlationService)
        {
            this._logger = logger;
            this._correlationService = correlationService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
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

                this._logger.LogError(ex, $"TrackId:{this._correlationService.CorrelationId} {errorMessage}");

                return (TResponse)Result.Failure(new List<string> { errorMessage });
            }
        }
    }
}
