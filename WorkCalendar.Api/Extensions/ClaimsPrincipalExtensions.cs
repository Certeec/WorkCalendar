using System.Security.Claims;

namespace MedicalCompanyManagement.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(userIdString, out int userId))
            {
                return userId;
            }

            throw new UnauthorizedAccessException("Didnt found correct ID in (NameIdentifier).");
        }
    }
}