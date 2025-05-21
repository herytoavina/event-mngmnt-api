using System.Security.Claims;
using EventManagementAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RegistrationController : ControllerBase
{
    
    private readonly IRegistrationService _registrationService;
    private readonly ILogger<RegistrationController> _logger;

    public RegistrationController(
        IRegistrationService registrationService,
        ILogger<RegistrationController> logger)
    {
        _registrationService = registrationService;
        _logger = logger;
    }
    
    [HttpPost("events/{eventId}")]
    public async Task<IActionResult> RegisterForEvent(int eventId)
    {
        var userId = GetCurrentUserId();
        if (userId == 0) return Unauthorized();

        var result = await _registrationService.RegisterForEventAsync(eventId, userId);
        
        return result.IsSuccess 
            ? Ok(new { Message = result.Message, RegistrationId = result.Registration }) 
            : BadRequest(result.Message);
    }
    
    [HttpDelete("events/{eventId}")]
    public async Task<IActionResult> CancelRegistration(int eventId)
    {
        var userId = GetCurrentUserId();
        if (userId == 0) return Unauthorized();

        var success = await _registrationService.CancelRegistrationAsync(eventId, userId);
        return success ? NoContent() : NotFound();
    }


    [HttpGet("user-registrations")]
    public async Task<IActionResult> GetUserRegistrations()
    {
        var userId = GetCurrentUserId();
        if (userId == 0) return Unauthorized();

        var registrations = await _registrationService.GetUserRegistrationsAsync(userId);
        return Ok(registrations);
    }
    
    private int GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(userIdClaim, out var userId) ? userId : 0;
    }
}