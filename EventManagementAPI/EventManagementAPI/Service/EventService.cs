using EventManagementAPI.DataContext;
using EventManagementAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace EventManagementAPI.Service;

public class EventService : IEventService
{
    private readonly AppDBContext _context;

    public EventService(AppDBContext context)
    {
        _context = context;
    }

    public async Task<Event> GetEventByIdAsync(long id)
    {
        return await _context.Events
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task<Event> CreateEventAsync(Event newEvent)
    {
        newEvent.Status = EventStatus.Draft;
        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();
        return newEvent;
    }

    public async Task<Event> UpdateEventAsync(long id, Event updatedEvent)
    {
        var existingEvent = await GetEventByIdAsync(id);
        if (existingEvent == null) return null;
        
        if (existingEvent.Status == EventStatus.Canceled)
            throw new InvalidOperationException("Cannot modify canceled events");

        existingEvent.Title = updatedEvent.Title;
        existingEvent.Description = updatedEvent.Description;
        existingEvent.Date = updatedEvent.Date;
        existingEvent.Location = updatedEvent.Location;
        existingEvent.Category = updatedEvent.Category;
        existingEvent.Capacity = updatedEvent.Capacity;

        await _context.SaveChangesAsync();
        return existingEvent;
    }

    public async Task<bool> DeleteEventAsync(long id)
    {
        var existingEvent = await GetEventByIdAsync(id);
        if (existingEvent == null) return false;

        existingEvent.Status = EventStatus.Canceled;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Event>> GetFilteredEventsAsync(DateTime? startDate, DateTime? endDate, string? category, EventStatus? status)
    {
        var query = _context.Events.AsQueryable();

        if (startDate.HasValue)
            query = query.Where(e => e.Date >= startDate.Value);
        
        if (endDate.HasValue)
            query = query.Where(e => e.Date <= endDate.Value);
        
        if (!string.IsNullOrEmpty(category))
            query = query.Where(e => e.Category == category);
        
        if (status.HasValue)
            query = query.Where(e => e.Status == status.Value);

        return await query.ToListAsync();
    }

    public async Task<bool> ChangeEventStatusAsync(long eventId, EventStatus newStatus)
    {
        var existingEvent = await GetEventByIdAsync(eventId);
        if (existingEvent == null) return false;
        
        if (existingEvent.Status == EventStatus.Canceled)
            return false;

        existingEvent.Status = newStatus;
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<List<Registration>> GetEventRegistrationsAsync(long eventId)
    {
        return await _context.Registrations
            .Where(r => r.EventId == eventId)
            .Include(r => r.User)
            .ToListAsync();
    }

    public async Task<(bool CanRegister, string Message)> CanRegisterToEventAsync(long eventId)
    {
        var existingEvent = await GetEventByIdAsync(eventId);
        
        if (existingEvent == null)
        return (false, "Event not found");
        
        if (existingEvent.Status != EventStatus.Published)
        return (false, "Event is not published");
        
        if (existingEvent.Registrations.Count >= existingEvent.Capacity)
        return (false, "Event is full");

        return (true, "Registration available");
    }
}