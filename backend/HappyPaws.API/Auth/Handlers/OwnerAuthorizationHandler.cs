using HappyPaws.Core.Entities.Common;
using HappyPaws.Core.Enums;
using Microsoft.AspNetCore.Authorization;

namespace HappyPaws.API.Auth.Handlers
{
    public class OwnerAuthorizationHandler : AuthorizationHandler<OwnerRequirement, IUserOwnedResource>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnerRequirement requirement, IUserOwnedResource resource)
        {
            if (context.User.IsInRole(UserType.Admin.ToString()) || context.User.FindFirst("UserId")?.Value == resource.UserId.ToString())
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
    public record OwnerRequirement : IAuthorizationRequirement;
}
