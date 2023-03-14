using core_application.Common;
using domain.Abstractions;
using FluentValidation;
using MediatR;

namespace application.Features.Commands
{
    public static class PasswordRenew
    {
        #region Command
        public class Command : IRequest<Result>
        {
            public int UserId { get; set; }
            public string NewPassword { get; set; }
        }

        #endregion

        #region Command Handler
        public class CommandHandler : IRequestHandler<Command, Result>
        {
            private readonly IUnitOfWork _uow;
            public CommandHandler(IUnitOfWork uow)
            {
                this._uow = uow; 
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var existedUser = this._uow.UserRepo.GetById(request.UserId);

                existedUser.ChangePassword(request.NewPassword);

                return Result.Success();
            }
        }
        #endregion

        #region Data Validation
        public class PasswordRenewCommand : AbstractValidator<Command>
        {
            public PasswordRenewCommand()
            {
                RuleFor(c => c.UserId).NotEqual(0).WithMessage("UserId alanı boş olamaz");
                RuleFor(c => c.NewPassword).NotEmpty().WithMessage("NewPassword alanı boş olamaz");
            }
        }
        #endregion 
    }
}
