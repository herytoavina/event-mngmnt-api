using EventManagementAPI.DataContext;
using EventManagementAPI.Model;
using EventManagementAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IAuthService _authService;
    private readonly AppDBContext _context;

    public LoginController(ITokenService tokenService, IAuthService authService, AppDBContext context)
    {
        _tokenService = tokenService;
        _authService = authService;
        _context = context;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] User model)
    {
        var user = await _authService.AuthenticateUser(model.Username, model.Password);
        
        if (user == null) return Unauthorized();
        var token = _tokenService.GenerateToken(user);
        return Ok(new { Token = token });
    }
    
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] User model)
    {
        if (await _context.Users.AnyAsync(u => u.Username == model.Username))
            return BadRequest("Username already exists");
        
        var user = await _authService.RegisterUser(model.Username, model.Password);
        var roles = user.Roles.Select(r => r.Name).ToList();
        return Ok(roles);
    }

}