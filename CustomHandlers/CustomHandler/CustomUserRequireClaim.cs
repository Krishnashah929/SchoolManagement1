using Microsoft.AspNetCore.Authorization;  
using System.Linq;  
using System.Threading.Tasks;

namespace CustomHandlers.CustomHandler
{
    public class CustomUserRequireClaim : IAuthorizationRequirement
    {
        public string ClaimType { get; }
        public CustomUserRequireClaim(string claimType)
        {
            ClaimType = claimType;
        }
    }
}