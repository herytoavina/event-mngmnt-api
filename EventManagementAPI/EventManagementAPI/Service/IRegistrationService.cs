using EventManagementAPI.Model;
using EventManagementAPI.Model.DTO;

namespace EventManagementAPI.Service;

public interface IRegistrationService
{
    Task<RegistrationResult> RegisterForEventAsync(int eventId, int userId);
    Task<bool> CancelRegistrationAsync(int eventId, int userId);
    Task<bool> IsUserRegisteredAsync(int eventId, int userId);
    Task<List<UserRegistrationDto>> GetUserRegistrationsAsync(int userId);
}

public record RegistrationResult(bool IsSuccess, string Message, Registration? Registration)
{
    public static RegistrationResult Successful(Registration registration) 
        => new(true, "Registration successful", registration);
    
    public static RegistrationResult Failed(string message) 
        => new(false, message, null);
}