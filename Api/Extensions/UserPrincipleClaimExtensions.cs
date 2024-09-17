using System.Security.Claims;

namespace Api.Extensions
{
    public static class UserPrincipleClaimExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {

            return user.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}
