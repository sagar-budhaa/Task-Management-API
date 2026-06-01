using System.ComponentModel.DataAnnotations;

namespace Task_Management_API.DTOs;

public class UserRegisterResponsetDto
{
    public bool Success { get; set; }

    [Required]
    public string Message { get; set; } = null!;
}