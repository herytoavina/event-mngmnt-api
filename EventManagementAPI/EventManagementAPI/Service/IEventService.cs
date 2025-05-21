using EventManagementAPI.Model;

namespace EventManagementAPI.Service;

public interface IEventService
{
    Task<Event> GetEventByIdAsync(long id);
    Task<Event> CreateEventAsync(Event newEvent);
    Task<Event> UpdateEventAsync(long id, Event updatedEvent);
    Task<bool> DeleteEventAsync(long id);
    Task<List<Event>> GetFilteredEventsAsync(DateTime? startDate, DateTime? endDate, string? category, EventStatus? status);
    Task<bool> ChangeEventStatusAsync(long eventId, EventStatus newStatus);
    Task<List<Registration>> GetEventRegistrationsAsync(long eventId);
    Task<(bool CanRegister, string Message)> CanRegisterToEventAsync(long eventId);
}