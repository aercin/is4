using domain.Abstractions;
using MediatR;

namespace application
{
    public static class UserRegistration
    {
        #region Command
        public class Command : IRequest<Response>
        {
            public int UserId { get; set; }
            public string FirstName { get; set; }
            public string FamilyName { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
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

                existedUser.ModifyUser(request.FirstName,request.FamilyName,request.UserName,request.Password);
                  
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
