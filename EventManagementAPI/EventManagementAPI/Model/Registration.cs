namespace EventManagementAPI.Model;

public class Registration
{
    
    public long UserId { get; set; }
    public long EventId { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    
    public User User { get; set; }
    public Event Event { get; set; }
}