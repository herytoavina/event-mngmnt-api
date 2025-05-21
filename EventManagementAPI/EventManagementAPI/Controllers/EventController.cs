using System.ComponentModel.DataAnnotations;
using EventManagementAPI.Model;
using EventManagementAPI.Model.DTO;
using EventManagementAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly ILogger<EventController> _logger;

    public EventController(IEventService eventService, ILogger<EventController> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllEvents(
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] string? category,
        [FromQuery] EventStatus? status)
    {
        try
        {
            var events = await _eventService.GetFilteredEventsAsync(startDate, endDate, category, status);
            return Ok(events);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving events");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetEvent(long id)
    {
        try
        {
            var eventItem = await _eventService.GetEventByIdAsync(id);
            return eventItem != null ? Ok(eventItem) : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving event with ID {id}");
            return StatusCode(500, "Internal server error");
        }
    }

   
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto eventDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newEvent = new Event
            {
                Title = eventDto.Title,
                Description = eventDto.Description,
                Date = eventDto.Date,
                Location = eventDto.Location,
                Category = eventDto.Category,
                Capacity = eventDto.Capacity
            };

            var createdEvent = await _eventService.CreateEventAsync(newEvent);
            return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.Id }, createdEvent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating event");
            return StatusCode(500, "Internal server error");
        }
    }

    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateEvent(long id, [FromBody] UpdateEventDto eventDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingEvent = await _eventService.GetEventByIdAsync(id);
            if (existingEvent == null)
                return NotFound();

            var updatedEvent = new Event
            {
                Id = id,
                Title = eventDto.Title,
                Description = eventDto.Description,
                Date = eventDto.Date,
                Location = eventDto.Location,
                Category = eventDto.Category,
                Capacity = eventDto.Capacity,
                Status = existingEvent.Status
            };

            var result = await _eventService.UpdateEventAsync(id, updatedEvent);
            return result != null ? Ok(result) : BadRequest("Could not update event");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating event with ID {id}");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPatch("{id}/status")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> UpdateEventStatus(long id, [FromBody] UpdateEventStatusDto statusDto)
    {
        try
        {
            var result = await _eventService.ChangeEventStatusAsync(id, statusDto.NewStatus);
            return result ? NoContent() : BadRequest("Invalid status change");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating status for event with ID {id}");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteEvent(long id)
    {
        try
        {
            var result = await _eventService.DeleteEventAsync(id);
            return result ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting event with ID {id}");
            return StatusCode(500, "Internal server error");
        }
    }
   
    [HttpGet("{id}/registrations")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetEventRegistrations(long id)
    {
        try
        {
            var registrations = await _eventService.GetEventRegistrationsAsync(id);
            return Ok(registrations);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving registrations for event with ID {id}");
            return StatusCode(500, "Internal server error");
        }
    }
}
