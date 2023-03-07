using application.Common;
using AutoMapper;
using domain.Abstractions;
using domain.Entities;
using FluentValidation;
using MediatR;

namespace application.Features.Commands
{
    public static class AddPermission
    {
        #region Command
        public class Command : IRequest<Result>
        {
            public string Scope { get; set; }
            public string Description { get; set; }
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
                var newPermission = Permission.AddPermission(request.Scope, request.Description);

                this._uow.PermissionRepo.Add(newPermission);

                await this._uow.CompleteAsync();

                return Result.Success();
            }
        }
        #endregion 

        #region Data Validation
        public class AddPermissionCommand : AbstractValidator<Command>
        {
            public AddPermissionCommand()
            {
                RuleFor(c => c.Scope).NotEmpty().WithMessage("Scope alanı boş olamaz");
                RuleFor(c => c.Description).NotEmpty().WithMessage("Description alanı boş olamaz");
            }
        }
        #endregion 
    }
}
