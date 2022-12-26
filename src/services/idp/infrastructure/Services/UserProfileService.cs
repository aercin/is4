using IdentityServer4.Models;
using IdentityServer4.Services;

namespace infrastructure.Services
{
    internal class UserProfileService : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.IssuedClaims.AddRange(context.Subject.Claims);
            await Task.CompletedTask;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            await Task.CompletedTask;
        }
    }
}
