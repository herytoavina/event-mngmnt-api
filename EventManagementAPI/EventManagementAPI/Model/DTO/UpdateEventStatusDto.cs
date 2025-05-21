using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.Model.DTO;

public class UpdateEventStatusDto
{
    [Required]
    public EventStatus NewStatus { get; set; }
}