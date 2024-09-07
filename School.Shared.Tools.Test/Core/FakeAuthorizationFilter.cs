using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace School.Shared.Tools.Test.Core;

public class FakeAuthorizationFilter : IAsyncActionFilter
{
    private readonly ClaimsPrincipal _principal;

    public FakeAuthorizationFilter(ClaimsPrincipal? principal = null)
    {
        _principal = principal ?? new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        {
            new("iid", Guid.NewGuid().ToString()),
            new("uid", Guid.NewGuid().ToString()),
            new("sub", "Tester"),
            new("systems:website:role", "Admin"),
            new("systems:game:role", "Admin"),
        }));
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        context.HttpContext.User = _principal;

        await next();
    }
}