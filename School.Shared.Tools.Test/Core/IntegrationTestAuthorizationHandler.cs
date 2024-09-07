using Microsoft.AspNetCore.Authorization;
using School.Shared.Core.Authentication.Claims;

namespace School.Shared.Tools.Test.Core;

public class IntegrationTestAuthorizationHandler : AuthorizationHandler<ClaimRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimRequirement requirement)
    {
        foreach (var authorizationRequirement in context.PendingRequirements.ToList())
        {
            context.Succeed(authorizationRequirement);
        }

        return Task.CompletedTask;
    }
}