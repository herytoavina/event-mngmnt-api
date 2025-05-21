using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.Model.DTO;

public class CreateEventDto
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [StringLength(1000)]
    public string Description { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [StringLength(200)]
    public string Location { get; set; }

    [Required]
    [StringLength(50)]
    public string Category { get; set; }

    [Range(1, 10000)]
    public int Capacity { get; set; }
}