namespace EventManagementAPI.Service;

public interface ITokenService
{
    public string GenerateToken(string username, List<string> roles);
}