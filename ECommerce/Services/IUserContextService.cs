using System.Security.Claims;

namespace ECommerce.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int GetUserId { get; }
    }
}