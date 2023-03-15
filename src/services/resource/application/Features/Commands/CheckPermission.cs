using core_application.Abstractions;
using core_application.Common;
using MediatR;

namespace application.Features.Commands
{
    public static class CheckPermission
    {
        #region Command
        [SkipBehaviour]
        public class Command : IRequest<Result>
        {
            public string Permission { get; set; }
        }
        #endregion

        #region Command Handler
        public class CommandHandler : IRequestHandler<Command, Result>
        {
            private readonly IHttpContextService _httpContextService;
            public CommandHandler(IHttpContextService httpContextService)
            {
                this._httpContextService = httpContextService;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(this._httpContextService.isClaimExist(request.Permission)
                                                                                                  ? Result.Success()
                                                                                                  : Result.Failure(new List<string> { "Yetkisiz işlem"}));
            }
        }
        #endregion 
    }
}
