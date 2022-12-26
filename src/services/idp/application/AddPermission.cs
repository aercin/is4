using domain.Abstractions;
using domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace application
{
    public static class AddPermission
    {
        #region Command
        public class Command : IRequest<Response>
        {
            public string Scope { get; set; }
            public string Description { get; set; }
        }

        #endregion

        #region Command Handler
        public class CommandHandler : IRequestHandler<Command, Response>
        {
            private readonly IUnitOfWork _uow;
            public CommandHandler(IUnitOfWork uow)
            {
                this._uow = uow;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var newPermission = Permission.AddPermission(request.Scope, request.Description);

                this._uow.PermissionRepo.Add(newPermission);

                await this._uow.CompleteAsync();

                return new Response { isSuccess = true };
            }
        }
        #endregion

        #region Response
        public class Response
        {
            public bool isSuccess { get; set; }
        }
        #endregion
    }
}
