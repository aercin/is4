using core_application.Common;
using domain.Abstractions;
using FluentValidation;
using MediatR;

namespace application.Features.Commands
{
    public static class AddUserPermission
    {
        #region Command
        public class Command : IRequest<Result>
        {
            public string Email { get; set; }
            public int PermissionId { get; set; }
        }

        #endregion

        #region Command Handler
        public class CommandHandler : IRequestHandler<Command, Result>
        {
            private readonly IUnitOfWork _uow;
            private readonly IPersistentGrantRepository _persistedGrantRepo;

            public CommandHandler(IUnitOfWork uow, IPersistentGrantRepository persistedGrantRepo)
            {
                this._uow = uow;
                this._persistedGrantRepo = persistedGrantRepo;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var existedUser = this._uow.UserRepo.Find(x => x.Email == request.Email).Single();

                existedUser.AddUserPermission(request.PermissionId);

                await this._uow.CompleteAsync();

                //Kullanıcıya yeni bir permission atandığında, refresh tokenı fiziksel olarak kaldırıyoruz. Böylelikle access token expired olduğunda kullanıcı tekrar sisteme kendini doğrulatarak access token talep etsin.
                //UI davranışın olduğu senaryoda access token expired olduğunda refresh tokenda olmadığı için silent sign in artık imkansız olacak kullanıcı login sayfasına yönlendirelecek
                //ve tekrar kendini tanıtarak access token talep etmesi istenecektir.
                await this._persistedGrantRepo.RemovePersistedGrant(existedUser.ClientId, existedUser.Id);

                return Result.Success();
            }
        }
        #endregion

        #region Data Validation
        public class AddUserPermissionCommand : AbstractValidator<Command>
        {
            public AddUserPermissionCommand()
            {
                RuleFor(c => c.PermissionId).NotEqual(0).WithMessage("PermissionId alanı boş olamaz");
                RuleFor(c => c.Email).NotEmpty().WithMessage("Email alanı boş olamaz")
                                     .EmailAddress().WithMessage("Email adres formatı uygun değildir");
            }
        }
        #endregion 
    }
}
