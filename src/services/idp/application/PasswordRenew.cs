using domain.Abstractions;
using MediatR;

namespace application
{
    public static class PasswordRenew
    {
        #region Command
        public class Command : IRequest<Response>
        {
            public int UserId { get; set; }
            public string NewPassword { get; set; }
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
                var existedUser = this._uow.UserRepo.GetById(request.UserId);

                existedUser.ChangePassword(request.NewPassword);

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
