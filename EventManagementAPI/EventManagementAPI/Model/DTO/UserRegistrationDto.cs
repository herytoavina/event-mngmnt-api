namespace EventManagementAPI.Model.DTO;

public class UserRegistrationDto
{
    public long EventId { get; set; }
    public string EventTitle { get; set; }
    public DateTime EventDate { get; set; }
    public string Location { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string EventStatus { get; set; }
}