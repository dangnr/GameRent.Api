using GameRent.Domain.Enums;
using GameRent.Domain.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace GameRent.Api.Extensions
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        public RoleAuthorizeAttribute(params UserRoleType[] roles)
        {
            Roles = SetStringRoles(roles);
        }

        private static string SetStringRoles(UserRoleType[] roles) => string.Join(",", roles.Select(x => x.GetDescription()));
    }
}