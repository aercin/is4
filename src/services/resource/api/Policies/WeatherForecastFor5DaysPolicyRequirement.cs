using IdentityModel;
using Microsoft.AspNetCore.Authorization;

namespace api.Policies
{
    public class WeatherForecastFor5DaysPolicyRequirement : IAuthorizationRequirement
    {
    }

    public class WeatherForecastFor5DaysPolicyHandler : AuthorizationHandler<WeatherForecastFor5DaysPolicyRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, WeatherForecastFor5DaysPolicyRequirement requirement)
        {
            var validScopes = new List<string> { "secure-api-forecast-for-five-days", "secure-api-full-access" };
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
