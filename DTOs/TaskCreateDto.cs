using System.ComponentModel.DataAnnotations;

namespace Task_Management_API.DTOs;

public class TaskCreateDto
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Name { get; set; }

    public bool IsActive { get; set; }
}