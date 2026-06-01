using System.ComponentModel.DataAnnotations;

namespace Task_Management_API.DTOs;

public class ProjectUpdateDto
{
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(2000)]
    public string Description { get; set; } = null!;

    [StringLength(100)]
    public string? Status { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime? EndDate { get; set; }

    public bool IsActive { get; set; }
}