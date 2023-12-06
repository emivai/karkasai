using HappyPaws.Core.Entities;
using HappyPaws.Core.Entities.Common;
using HappyPaws.Core.Enums;
using Microsoft.AspNetCore.Authorization;

namespace HappyPaws.API.Auth.Handlers
{
        public class SameUserAuthorizationHandler : AuthorizationHandler<SameUserRequirement, User>
        {
            protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameUserRequirement requirement, User user)
            {
                if (context.User.IsInRole(UserType.Admin.ToString()) || context.User.FindFirst("UserId")?.Value == user.Id.ToString())
                {
                    context.Succeed(requirement);
                }
                return Task.CompletedTask;
            }
        }
        public record SameUserRequirement : IAuthorizationRequirement;
}
