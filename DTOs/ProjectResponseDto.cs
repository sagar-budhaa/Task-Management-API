using System.ComponentModel.DataAnnotations;

namespace Task_Management_API.DTOs;

public class ProjectResponseDto
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public string Status { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
}