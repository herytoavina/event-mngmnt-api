namespace EventManagementAPI.Model;

public class Event
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public string? Location { get; set; }
    public string? Category { get; set; }
    public int Capacity { get; set; }
    public EventStatus Status { get; set; } = EventStatus.Draft;
    public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
public enum EventStatus { Draft, Published, Canceled }



