using System.Text.Json.Serialization;

namespace EventManagementAPI.Model;

public class User
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public ICollection<Role> Roles { get; set; } = new List<Role>();
    [JsonIgnore]
    public ICollection<Registration> Registrations { get; set; } = new List<Registration>();

}