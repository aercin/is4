using IdentityModel;
using Microsoft.AspNetCore.Authorization;

namespace api.Policies
{
    public class WeatherForecastForInstantPolicyRequirement : IAuthorizationRequirement
    {
    }

    public class WeatherForecastForInstantPolicyHandler : AuthorizationHandler<WeatherForecastForInstantPolicyRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, WeatherForecastForInstantPolicyRequirement requirement)
        {
            var validScopes = new List<string> { "secure-api-instant-forecast", "secure-api-full-access" };
             
            if (context.User.HasClaim(c => c.Type == JwtClaimTypes.Subject))
            {//interactive

                if (context.User.Claims.Any(x => x.Type == JwtClaimTypes.Role
                                              && validScopes.Contains(x.Value)))
                {
                    context.Succeed(requirement);
                }
            }
            else
            {//m2m 
                if (context.User.Claims.Any(x => x.Type == JwtClaimTypes.Scope
                                              && validScopes.Contains(x.Value)))
                {
                    context.Succeed(requirement);
                }
            }

            await Task.CompletedTask;
        }
    }
}
