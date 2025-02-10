using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace DfE.ManageSchoolImprovement.Frontend.Authorization;

public class ClaimsRequirementHandler(IHostEnvironment environment,
                                IHttpContextAccessor httpContextAccessor,
                                IConfiguration configuration) : AuthorizationHandler<ClaimsAuthorizationRequirement>, IAuthorizationRequirement
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimsAuthorizationRequirement requirement)
    {
        if (HeaderRequirementHandler.ClientSecretHeaderValid(environment, httpContextAccessor, configuration))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
