using IdentityModel;
using Microsoft.AspNetCore.Authorization;

namespace api.Policies
{
    public class WeatherForecastFor10DaysPolicyRequirement : IAuthorizationRequirement
    {
    }

    public class WeatherForecastFor10DaysPolicyHandler : AuthorizationHandler<WeatherForecastFor10DaysPolicyRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, WeatherForecastFor10DaysPolicyRequirement requirement)
        {
            var validScopes = new List<string> { "secure-api-forecast-for-ten-days", "secure-api-full-access" };
             
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
