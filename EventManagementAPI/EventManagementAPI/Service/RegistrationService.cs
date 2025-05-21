using EventManagementAPI.DataContext;
using EventManagementAPI.Model;
using EventManagementAPI.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace EventManagementAPI.Service;

public class RegistrationService : IRegistrationService
{
    private readonly AppDBContext _context;
    private readonly IEventService _eventService;
    private readonly ILogger<RegistrationService> _logger;

    public RegistrationService(
        AppDBContext context,
        IEventService eventService,
        ILogger<RegistrationService> logger)
    {
        _context = context;
        _eventService = eventService;
        _logger = logger;
    }

    public async Task<RegistrationResult> RegisterForEventAsync(int eventId, int userId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        
        try
        {
            // Check existing registration
            if (await IsUserRegisteredAsync(eventId, userId))
                return RegistrationResult.Failed("User already registered for this event");

            // Check event validity
            var (canRegister, message) = await _eventService.CanRegisterToEventAsync(eventId);
            if (!canRegister)
                return RegistrationResult.Failed(message);

            // Create registration
            var registration = new Registration
            {
                UserId = userId,
                EventId = eventId,
                RegistrationDate = DateTime.UtcNow
            };

            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return RegistrationResult.Successful(registration);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Error registering user {UserId} for event {EventId}", userId, eventId);
            return RegistrationResult.Failed("Registration failed due to server error");
        }
    }

    public async Task<bool> CancelRegistrationAsync(int eventId, int userId)
    {
        try
        {
            var registration = await _context.Registrations
                .FirstOrDefaultAsync(r => r.EventId == eventId && r.UserId == userId);

            if (registration == null) return false;

            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error canceling registration for user {UserId} on event {EventId}", userId, eventId);
            return false;
        }
    }

    public async Task<bool> IsUserRegisteredAsync(int eventId, int userId)
    {
        return await _context.Registrations
            .AnyAsync(r => r.EventId == eventId && r.UserId == userId);
    }

    public async Task<List<UserRegistrationDto>> GetUserRegistrationsAsync(int userId)
    {
        return await _context.Registrations
            .Where(r => r.UserId == userId)
            .Include(r => r.Event)
            .Select(r => new UserRegistrationDto
            {
                EventId = r.EventId,
                EventTitle = r.Event.Title,
                EventDate = r.Event.Date,
                Location = r.Event.Location,
                RegistrationDate = r.RegistrationDate,
                EventStatus = r.Event.Status.ToString()
            })
            .ToListAsync();
    }
}