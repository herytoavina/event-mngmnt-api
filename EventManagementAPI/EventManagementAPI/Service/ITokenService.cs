using EventManagementAPI.Model;

namespace EventManagementAPI.Service;

public interface ITokenService
{
    public string GenerateToken(User user);
}