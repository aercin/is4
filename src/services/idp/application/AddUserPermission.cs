using domain.Abstractions;
using MediatR;

namespace application
{
    public static class AddUserPermission
    {
        #region Command
        public class Command : IRequest<Response>
        {
            public string Email { get; set; }
            public int PermissionId { get; set; }
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
                var existedUser = this._uow.UserRepo.Find(x => x.Email == request.Email).Single();

                existedUser.AddUserPermission(request.PermissionId);

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
