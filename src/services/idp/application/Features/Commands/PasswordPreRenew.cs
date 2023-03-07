using application.Common;
using domain.Abstractions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace application.Features.Commands
{
    public static class PasswordPreRenew
    {
        #region Command
        public class Command : IRequest<Result>
        {
            public string Email { get; set; }
        }

        #endregion

        #region Command Handler
        public class CommandHandler : IRequestHandler<Command, Result>
        {
            private readonly IUnitOfWork _uow;
            private readonly IClientRepository _clientRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;
            public CommandHandler(IUnitOfWork uow, IClientRepository clientRepository, IHttpContextAccessor httpContextAccessor)
            {
                this._uow = uow;
                this._clientRepository = clientRepository;
                this._httpContextAccessor = httpContextAccessor;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var existedUser = this._uow.UserRepo.Find(x => x.Email == request.Email).Single();

                //Senaryo gereği; bu aşamada access token payload da yer alan client_id bilgisiyle ilişkili change password adresine data storedan erişip
                //kullanıcının userid'sini query item olarak bu url'e ekleyerek, kullanıcının mail adresine mail gönderilmesi gerekir. 
                //Poc olduğu için sadece client redirect url bilgisini data storedan alip sanki mail gonderilmis gibi farzedilecektir.
                var clientId = this._httpContextAccessor.HttpContext.User.Claims.Single(x => x.Type == "client_id").Value;
                var clientRedirectUrl = this._clientRepository.GetClientRedirectUrl(clientId, "password");

                return Result.Success();
            }
        }
        #endregion

        #region Data Validation
        public class PasswordPreRenewCommand : AbstractValidator<Command>
        {
            public PasswordPreRenewCommand()
            {
                RuleFor(c => c.Email).NotEmpty().WithMessage("Email alanı boş olamaz")
                                     .EmailAddress().WithMessage("Email adres formatı uygun değildir");
            }
        }
        #endregion 
    }
}
