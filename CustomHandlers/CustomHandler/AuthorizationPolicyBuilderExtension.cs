using Microsoft.AspNetCore.Authorization;

namespace CustomHandlers.CustomHandler
{
    /// <summary>
    /// class for Building authorization policy 
    /// </summary>
    public static class AuthorizationPolicyBuilderExtension
    {
        public static AuthorizationPolicyBuilder UserRequireCustomClaim(this AuthorizationPolicyBuilder builder, string claimType)
        {
            builder.AddRequirements(new CustomUserRequireClaim(claimType));
            return builder;
        }
    }
}