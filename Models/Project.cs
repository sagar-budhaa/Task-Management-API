using System.ComponentModel.DataAnnotations;

namespace Task_Management_API.Models;

public class Project
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Status { get; set; }
    
    public List<Task> Tasks { get; set; } = [];
    
    public Guid UserId { get; set; }
    public User? User { get; set; }
}