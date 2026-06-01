using System.ComponentModel.DataAnnotations;

namespace Task_Management_API.Models;

public class User
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    [StringLength(100)]
    public string? Username { get; set; }
    public required string? Password { get; set; }
    public DateTime created_at { get; set; } = DateTime.UtcNow;
    
    public List<Project> Projects { get; set; } = [];
}