using core_application.Abstractions;
using core_application.Common;
using domain.Abstractions;
using FluentValidation;
using MediatR;

namespace application.Features.Commands
{
    public static class UserRegistration
    {
        #region Command
        public class Command : IRequest<Result>
        {
            public int UserId { get; set; }
            public string FirstName { get; set; }
            public string FamilyName { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        #endregion

        #region Command Handler
        public class CommandHandler : IRequestHandler<Command, Result>
        {
            private readonly IUnitOfWork _uow;
            private readonly ISecurityService _securityService;
            public CommandHandler(IUnitOfWork uow, ISecurityService securityService)
            {
                this._uow = uow;
                this._securityService = securityService;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var existedUser = this._uow.UserRepo.GetById(request.UserId);

                existedUser.ModifyUser(request.FirstName, request.FamilyName, request.UserName, this._securityService.HashPassword(request.Password));

                await this._uow.CompleteAsync();

                return Result.Success();
            }
        }
        #endregion

        #region Data Validation
        public class UserRegistrationCommand : AbstractValidator<Command>
        {
            public UserRegistrationCommand()
            {
                RuleFor(c => c.UserId).NotEqual(0).WithMessage("UserId alanı boş olamaz");
                RuleFor(c => c.FirstName).NotEmpty().WithMessage("FirstName alanı boş olamaz");
                RuleFor(c => c.FamilyName).NotEmpty().WithMessage("FamilyName alanı boş olamaz");
                RuleFor(c => c.UserName).NotEmpty().WithMessage("UserName alanı boş olamaz");
                RuleFor(c => c.Password).NotEmpty().WithMessage("Password alanı boş olamaz");
            }
        }
        #endregion  
    }
}
