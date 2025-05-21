using EventManagementAPI.DataContext;
using EventManagementAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventManagementAPI.Service;

public class AuthService : IAuthService
{
    private readonly AppDBContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(AppDBContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> AuthenticateUser(string username, string password)
    {
        var user = await _context.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user == null)
            return null;

        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
        return result == PasswordVerificationResult.Success ? user : null;
    }
    
   /* public async Task<List<string>> GetUserRoles(int userId)
    {
        return await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.Role.Name)
            .ToListAsync();
    } */

    public async Task<User> RegisterUser(string username, string password)
    {
        var user = new User
        {
            Username = username
        };
        
        user.Password = _passwordHasher.HashPassword(user, password);
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        // Assign default "User" role
        var userRole = new UserRole
        {
            UserId = user.Id,
            RoleId = 1 // Default User role
        };
        
        _context.UserRoles.Add(userRole);
        await _context.SaveChangesAsync();
        
        return user;
    }
}