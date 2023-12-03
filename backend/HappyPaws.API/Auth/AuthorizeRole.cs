using HappyPaws.Core.Enums;
using Microsoft.AspNetCore.Authorization;

namespace HappyPaws.API.Auth
{
    public class AuthorizeRole : AuthorizeAttribute
    {
        public AuthorizeRole(params UserType[] roles)
        {
            var allowedRolesAsStrings = roles.Select(x => Enum.GetName(typeof(UserType), x)).ToList();
            allowedRolesAsStrings.Add(Enum.GetName(typeof(UserType), UserType.Admin));

            Roles = string.Join(",", allowedRolesAsStrings);
        }
    }
}
