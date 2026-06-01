using System.ComponentModel.DataAnnotations;

namespace Task_Management_API.DTOs;

public class TaskResponseDto
{
    public Guid Id { get; set; }

    [Required]
    public string? Name { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid ProjectId { get; set; }
}