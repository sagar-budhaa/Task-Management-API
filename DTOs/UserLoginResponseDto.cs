using System.ComponentModel.DataAnnotations;

namespace Task_Management_API.DTOs;

public class UserLoginResponseDto
{
    public bool Success { get; set; }

    [Required]
    public string AccessToken { get; set; } = null!;

    [Required]
    public string Message { get; set; } = null!;
}