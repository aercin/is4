using core_application.Abstractions;
using core_application.Common;
using domain.Abstractions;
using domain.Entities;
using FluentValidation;
using MediatR;

namespace application.Features.Commands
{
    public static class UserPreRegistration
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
            private readonly IHttpContextService _httpContextService;
            public CommandHandler(IUnitOfWork uow, IClientRepository clientRepository, IHttpContextService httpContextService)
            {
                this._uow = uow; 
                this._httpContextService = httpContextService;
                this._clientRepository = clientRepository;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var clientId = this._httpContextService.GetClaimValue("client_id");

                var newUser = User.AddUser(request.Email, clientId);

                this._uow.UserRepo.Add(newUser);

                await this._uow.CompleteAsync();

                //Senaryo gereği; bu aşamada access token payload da yer alan client_id bilgisiyle ilişkili user registration adresine data storedan erişip
                //pasif olarak kaydedilen kullanıcının userid'sini query item olarak bu url'e ekleyerek, kullanıcının mail adresine mail gönderilmesi gerekir. 
                //Poc olduğu için sadece client redirect url bilgisini data storedan alip sanki mail gonderilmis gibi farzedilecektir. 
                //var clientRedirectUrl = await this._clientRepository.GetClientRedirectUrl(clientId, "registration");

                return Result.Success();
            }
        }
        #endregion

        #region Data Validation
        public class UserPreRegistrationCommand : AbstractValidator<Command>
        {
            public UserPreRegistrationCommand()
            {
                RuleFor(c => c.Email).NotEmpty().WithMessage("Email alanı boş olamaz")
                                     .EmailAddress().WithMessage("Email adres formatı uygun değildir");
            }
        }
        #endregion 
    }
}
