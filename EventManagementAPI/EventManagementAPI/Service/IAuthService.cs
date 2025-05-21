using EventManagementAPI.Model;

namespace EventManagementAPI.Service;

public interface IAuthService
{
    public Task<User> AuthenticateUser(string username, string password);
    public Task<User> RegisterUser(string username, string password);
}