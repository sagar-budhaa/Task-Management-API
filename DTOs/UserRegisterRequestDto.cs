using System.ComponentModel.DataAnnotations;

namespace Task_Management_API.DTOs;

public class UserRegisterRequestDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Username { get; set; } = null!;

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = null!;
}