using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using SM.Entity;
using SM.Repositories.IRepository;
using System.Linq;
using System.Threading.Tasks;

namespace CustomHandlers.CustomHandler
{
    public class RolesAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler
    {
        private IUserRepository _userRepository;

        public RolesAuthorizationHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
                validRole = _userRepository.GetAll().Where(p => roles.Contains(p.Role) && p.EmailAddress == userEmail).Any();
                //validRole = new User().GetUsers().Where(p => roles.Contains(p.Role) && p.EmailAddress == userEmail).Any();

                //validRole = new User.Where(p => roles.Contains(p.Role) && p.EmaiAddress == UserEmail).Any();
            }

            if (validRole)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            else
            {
                return Task.FromResult(0);
                //return Task.CompletedTask;

            }
            //return Task.CompletedTask;
        }
    }
}