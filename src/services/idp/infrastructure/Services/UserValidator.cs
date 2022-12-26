using domain.Abstractions;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Security.Claims;

namespace infrastructure.Services
{
    internal class UserValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUnitOfWork _uow;
        public UserValidator(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existedUser = this._uow.UserRepo.GetWithPermissions(context.UserName, context.Password);

            if (existedUser != null)//valid state
            {
                var permissionClaims = new List<Claim>();
                existedUser.Permissions.ForEach(userPermission =>
                {
                    permissionClaims.Add(new Claim(JwtClaimTypes.Role, userPermission.Permission.Scope));
                });
               
                context.Result = new GrantValidationResult(
                    subject: existedUser.Id.ToString(),
                    authenticationMethod: "custom",
                    claims: permissionClaims
                //Kimlik doğrulamasından geçen kullanıcı permission claimleri bu aşamada token payload'a eklenmiyor.
                //UserProfileService içerisinde context.IssuedClaims kümesine burdan context.Subject.Claims alanı taşınan permission claimlerinin bağlanması gerekiyor.
                );
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.UnauthorizedClient, "Invalid Credentials");
            }

            await Task.CompletedTask;
        }
    }
}
