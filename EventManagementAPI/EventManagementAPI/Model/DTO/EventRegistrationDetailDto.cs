namespace EventManagementAPI.Model.DTO;

public class EventRegistrationDetailDto
{
    public long UserId { get; set; }
    public string UserName { get; set; }
    public DateTime RegistrationDate { get; set; }
}