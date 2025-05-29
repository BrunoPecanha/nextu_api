using System.Security.Claims;

namespace UFF.Domain.Services
{
    public interface ITokenService
    {
        string CreateToken(int customerId, int queueId);
        ClaimsPrincipal ValidateToken(string token);
    }
}
