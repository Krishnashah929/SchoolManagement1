using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using SM.Entity;
using SM.Services;
using System.Linq;
using System.Threading.Tasks;

namespace CustomHandlers.CustomHandler
{
    /// <summary>
    /// Checking that specified user is applicable on given authorization filter or not. 
    /// </summary>
    public class RolesAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler
    {
        private IUsersService _userServices;

        public RolesAuthorizationHandler(IUsersService userServices)
        {
            _userServices = userServices;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       RolesAuthorizationRequirement requirement)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask;
            }
            var validRole = false;
            if (requirement.AllowedRoles == null ||
                requirement.AllowedRoles.Any() == false)
            {
                validRole = true;
            }
            else
            {
                var claims = context.User.Claims;
                var userEmail = claims.FirstOrDefault(c => c.Type == "UserEmail").Value;
                var roles = requirement.AllowedRoles;
                validRole = _userServices.GetAllUser().Where(p => roles.Contains(p.Role) && p.EmailAddress == userEmail).Any();
            }
            if (validRole)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            else
            {
                return Task.FromResult(0);
            }
        }
    }
}